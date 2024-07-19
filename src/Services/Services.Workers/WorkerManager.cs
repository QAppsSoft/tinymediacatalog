using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Common;
using DynamicData;
using Services.Workers.Abstractions;
using Common.Extensions.Disposables;

namespace Services.Workers;

public class WorkerManager : IWorkerManager, IDisposable
{
    private readonly ISchedulerProvider _schedulerProvider;
    private readonly CompositeDisposable _cleanup = new();
    private readonly SourceList<IWorker> _workers = new();
    

    public WorkerManager(ISchedulerProvider schedulerProvider)
    {
        _schedulerProvider = schedulerProvider;
        Workers = _workers.Connect().DisposeMany().ToObservableChangeSet();

        var anyWorkerRunning = _workers.Connect()
            .AutoRefresh(worker => worker.Status)
            .Filter(worker => worker.Status is WorkerStatus.Started or WorkerStatus.Cancelling)
            .Count()
            .Select(x => x > 0)
            .StartWith(false);

        var idleWorkers = _workers.Connect()
            .AutoRefresh(worker => worker.Status)
            .Filter(worker => worker.Status is WorkerStatus.Iddle)
            .ToCollection()
            .StartWithEmpty();

        anyWorkerRunning.CombineLatest(idleWorkers)
            .Where(NotRunningAndSomeQueued)
            .Select(x => x.Second)
            .SelectMany(StartIdleWorker)
            .Subscribe()
            .DisposeWith(_cleanup);

        Enqueued = _workers.Connect().Count();
    }

    public void AddWorker(IWorker worker)
    {
        _workers.Add(worker);
    }

    private static bool NotRunningAndSomeQueued((bool First, IReadOnlyCollection<IWorker> Second) tuple)
    {
        var (anyRunning, idle) = tuple;
        return !anyRunning && idle.Count > 0;
    }

    public IObservable<IChangeSet<IChangeSet<IWorker>>> Workers { get; }
    public IObservable<int> Enqueued { get; }

    private IObservable<Unit> StartIdleWorker(IReadOnlyCollection<IWorker> idle)
    {
        return Observable.FromAsync(_ => idle.First().StartWorkerAsync(), _schedulerProvider.TaskPool);
    }

    public void Dispose()
    {
        _cleanup.Dispose();
        _workers.Dispose();
    }
}
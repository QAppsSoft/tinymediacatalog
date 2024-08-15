using DynamicData;

namespace Services.Workers.Abstractions;

public interface IWorkerManager
{
    void AddWorker(IWorker worker);
    IObservable<IChangeSet<IChangeSet<IWorker>>> Workers { get; }
    IObservable<int> Enqueued { get; }
}
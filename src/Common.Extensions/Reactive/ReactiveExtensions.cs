using System.Diagnostics;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;

namespace Common.Extensions.Reactive;

public static class ReactiveExtensions
{
    public static IObservable<T> Log<T>(this IObservable<T> source, ILogger logger, string name)
    {
        return Observable.Using(
            () => logger.Time(name),
            _ => Observable.Create<T>(
                o =>
                {
                    logger.LogDebug("{Name}.Subscribe()", name);
                    var subscription = source
                        .Do(
                            i => logger.LogTrace("{Name}.OnNext({@Value})", name, i),
                            ex => logger.LogError(ex, "{Name}.OnError({Type})", name, ex.GetType()),
                            () => logger.LogDebug("{Name}.OnCompleted()", name))
                        .Subscribe(o);
                    var disposal = Disposable.Create((Logger: logger, Name: name),tuple => tuple.Logger.LogDebug("{Name}.Dispose()", tuple.Name));
                    return new CompositeDisposable(subscription, disposal);
                })
        );
    }
    
    [DebuggerStepThrough]
    private static IDisposable Time(this ILogger logger, string name)
    {
        return new Timer(logger, name);
    }

    private sealed class Timer : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _name;
        private readonly Stopwatch _stopwatch;

        [DebuggerStepThrough]
        public Timer(ILogger logger, string name)
        {
            _logger = logger;
            _name = name;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.LogDebug("{Name} took {Duration}", _name, _stopwatch.Elapsed);
        }
    }
    
    // Only intended for Debug
    public static IObservable<T> Spy<T>(IObservable<T> source, string? opName, Action<string> logger)
    {
        opName ??= "IObservable";
        logger(
            $"{opName}: Observable obtained on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}");

        var count = 0;

        return Observable.Create<T>(obs =>
        {
            logger(
                $"{opName}: Subscribed to on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}");
            try
            {
                var subscription = source
                    .Do(
                        x => logger($"{opName}: OnNext({x}) on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}"),
                        ex => logger($"{opName}: OnError({ex}) on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}"),
                        () => logger($"{opName}: OnCompleted()  on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}"))
                    .Subscribe(t =>
                    {
                        try
                        {
                            obs.OnNext(t);
                        }
                        catch (Exception ex)
                        {
                            logger($"{opName}: Downstream exception({ex}) on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}");
                            throw;
                        }
                    }, obs.OnError, obs.OnCompleted);

                return new CompositeDisposable(
                    Disposable.Create(() =>
                        logger($"{opName}: Dispose (Unsubscribe or Observable finished) on Thread: {Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)}")),
                    subscription,
                    Disposable.Create(() => Interlocked.Decrement(ref count)),
                    Disposable.Create(count,
                        x => logger(
                            $"{opName}: Dispose (Unsubscribe or Observable finished) completed, {x.ToString(CultureInfo.InvariantCulture)} subscriptions")));
            }
            finally
            {
                Interlocked.Increment(ref count);
                logger($"{opName}: Subscription completed, {count.ToString(CultureInfo.InvariantCulture)} subscriptions.");
            }
        });
    }
}
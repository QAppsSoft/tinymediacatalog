using System.Diagnostics;
using System.Globalization;
using System.Reactive;
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
    public static IObservable<T> Spy<T>(IObservable<T> source, string? operationName, ILogger logger)
    {
        operationName ??= "IObservable";
        logger.LogDebug("{OperationName}: Observable obtained on Thread: {Thread}",
            operationName, Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture));

        var count = 0;

        return Observable.Create<T>(observer =>
        {
            logger.LogDebug("{OperationName}: Subscribed to on Thread: {Thread}",
                operationName, Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture));
            try
            {
                var subscription = source
                    .Do(
                        value => logger.LogTrace("{OperationName}: OnNext({Value}) on Thread: {Thread}",
                            operationName, value, Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)),
                        exception => logger.LogError(exception,
                            "{OperationName}: OnError({Exception}) on Thread: {Thread}",
                            operationName, exception.GetType(), Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)),
                        () => logger.LogDebug("{OperationName}: OnCompleted()  on Thread: {Thread}",
                            operationName, Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture)))
                    .Subscribe(value =>
                    {
                        try
                        {
                            observer.OnNext(value);
                        }
                        catch (Exception exception)
                        {
                            logger.LogError(exception,
                                "{OperationName}: Downstream exception({Exception}) on Thread: {Thread}",
                                operationName, exception.GetType(),
                                Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture));
                            throw;
                        }
                    }, observer.OnError, observer.OnCompleted);

                return new CompositeDisposable(
                    Disposable.Create(operationName, opName =>
                        logger.LogDebug(
                            "{OperationName}: Dispose (Unsubscribe or Observable finished) on Thread: {Thread}",
                            opName, Environment.CurrentManagedThreadId.ToString(CultureInfo.InvariantCulture))),
                    subscription,
                    Disposable.Create(() => Interlocked.Decrement(ref count)),
                    Disposable.Create(count,
                        number => logger.LogDebug(
                            "{OperationName}: Dispose (Unsubscribe or Observable finished) completed, {Count} subscriptions",
                            operationName, number.ToString(CultureInfo.InvariantCulture))));
            }
            finally
            {
                Interlocked.Increment(ref count);
                logger.LogDebug("{OperationName}: Subscription completed, {Count} subscriptions.",
                    operationName, count.ToString(CultureInfo.InvariantCulture));
            }
        });
    }

    public static IObservable<Unit> ToUnit<T>(this IObservable<T> source) => source.Select(_ => Unit.Default);
}
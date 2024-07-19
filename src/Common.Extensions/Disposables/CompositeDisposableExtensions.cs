using System.Reactive.Disposables;

namespace Common.Extensions.Disposables;

public static class CompositeDisposableExtensions
{
    public static void DisposeWith(this IDisposable observable, CompositeDisposable disposables)
    {
        disposables.Add(observable);
    }
}
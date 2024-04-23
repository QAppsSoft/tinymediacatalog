using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using ReactiveUI;
using Serilog;

namespace MediaManager;

public sealed class ReactiveUiObservableExceptionHandler : IObserver<Exception>
{
    public void OnNext(Exception value)
    {
        if (Debugger.IsAttached) Debugger.Break();

        Log.Fatal(value, "A global non caught exception happened");

        RxApp.MainThreadScheduler.Schedule(() => throw value) ;
    }

    public void OnError(Exception error)
    {
        if (Debugger.IsAttached) Debugger.Break();

        Log.Fatal(error, "A global non caught exception happened");

        RxApp.MainThreadScheduler.Schedule(() => throw error);
    }

    public void OnCompleted()
    {
        if (Debugger.IsAttached) Debugger.Break();
        RxApp.MainThreadScheduler.Schedule(() => throw new NotImplementedException());
    }
}
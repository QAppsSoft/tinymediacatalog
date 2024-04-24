using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using Serilog;

namespace MediaManager;

public class InternalAppBuilder(Func<AppBuilder> builder)
{
    private readonly Func<AppBuilder> _builder = builder ?? throw new ArgumentNullException(nameof(builder));

    public void Run(string[] args)
    {
        // Catch global unhandled exceptions
        RxApp.DefaultExceptionHandler = Observer.Create<Exception>(OnException);
        TaskScheduler.UnobservedTaskException += (_, eventArgs) => LogAndClose(eventArgs.Exception);
        
        try
        {
            // prepare and run your App here
            _builder().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception exception)
        {
            LogAndClose(exception);
        }
        finally
        {
            // This block is optional. 
            // Use the finally-block if you need to clean things up or similar
            Log.CloseAndFlush();
        }
    }

    private static void OnException(Exception exception)
    {
        if (Debugger.IsAttached) Debugger.Break();

        Log.Fatal(exception, "A global non caught exception happened");

        RxApp.MainThreadScheduler.Schedule(() => throw exception) ;
    }

    private static void LogAndClose(Exception exception)
    {
        Log.Fatal(exception, "A global non caught exception happened");

        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown(1);
        }
        else
        {
            throw exception;
        }
    }
}
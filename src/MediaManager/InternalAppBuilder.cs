using System;
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
        RxApp.DefaultExceptionHandler = new ReactiveUiObservableExceptionHandler();
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
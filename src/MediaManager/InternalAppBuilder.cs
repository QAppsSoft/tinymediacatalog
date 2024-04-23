using System;
using System.Threading.Tasks;
using Avalonia;
using ReactiveUI;
using Serilog;

namespace MediaManager;

public class InternalAppBuilder(Func<AppBuilder> builder)
{
    private readonly Func<AppBuilder> _builder = builder ?? throw new ArgumentNullException(nameof(builder));

    public void Run(string[] args)
    {
        RxApp.DefaultExceptionHandler = new ReactiveUiObservableExceptionHandler();
        TaskScheduler.UnobservedTaskException += (_, eventArgs) =>
        {
            // here we can work with the exception, for example add it to our log file
            Log.Fatal(eventArgs.Exception, "A global non caught exception happened");
        };

        try
        {
            // prepare and run your App here
            _builder().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            // here we can work with the exception, for example add it to our log file
            Log.Fatal(e, "A global non caught exception happened");
        }
        finally
        {
            // This block is optional. 
            // Use the finally-block if you need to clean things up or similar
            Log.CloseAndFlush();
        }
    }
}
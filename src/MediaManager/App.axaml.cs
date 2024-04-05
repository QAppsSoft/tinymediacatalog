using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MediaManager.Views;

namespace MediaManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            && Resources["Composition"] is Composition composition)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = composition.MainViewViewModel,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
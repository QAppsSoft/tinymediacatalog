using Common;
using MediaManager.Infrastructure;
using MediaManager.ViewModels;
using Pure.DI;

namespace MediaManager;

internal partial class Composition
{
    void Setup() => DI.Setup(nameof(Composition))

        // View Models
        .Bind().As(Lifetime.Singleton).To<MainViewViewModel>()
        .Bind().As(Lifetime.Singleton).To<LeftPaneViewModel>()
        .Bind().As(Lifetime.Singleton).To<RightPaneViewModel>()
        .Bind().As(Lifetime.Singleton).To<TitleBarViewModel>()

        // Models

        // Infrastructure
        .Bind<ISchedulerProvider>().As(Lifetime.Singleton).To<SchedulerProvider>()

        .Root<MainViewViewModel>("MainViewViewModel")
        .Root<Program>("Root");
}

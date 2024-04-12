using Common;
using MediaManager.Infrastructure;
using MediaManager.ViewModels;
using MediaManager.ViewModels.Interfaces;
using Pure.DI;

namespace MediaManager;

internal partial class Composition
{
    void Setup() => DI.Setup(nameof(Composition))

        // View Models
        .Bind().As(Lifetime.Singleton).To<MainViewViewModel>()
        .Bind().As(Lifetime.Singleton).To<TitleBarViewModel>()
        .Bind<IPageViewModel>().As(Lifetime.Singleton).To<MoviesViewModel>()

        // Models

        // Infrastructure
        .Bind<ISchedulerProvider>().As(Lifetime.Singleton).To<SchedulerProvider>()

        .Root<MainViewViewModel>("MainViewViewModel")
        .Root<Program>("Root");
}

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
        
        // Main menú
        .Bind<IPageViewModel>(1).As(Lifetime.Singleton).To<MoviesViewModel>()
        .Bind<IPageViewModel>(2).As(Lifetime.Singleton).To<MovieCollectionsViewModel>()
        .Bind<IPageViewModel>(3).As(Lifetime.Singleton).To<TvShowsViewModel>()

        // Models

        // Infrastructure
        .Bind<ISchedulerProvider>().As(Lifetime.Singleton).To<SchedulerProvider>()

        .Root<MainViewViewModel>("MainViewViewModel")
        .Root<Program>("Root");
}

using System.IO;
using Common;
using Common.Vars;
using Domain;
using Domain.Factories;
using MediaManager.DependencyInjection;
using MediaManager.Infrastructure;
using MediaManager.ViewModels;
using MediaManager.ViewModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pure.DI;
using Serilog;
using Serilog.Extensions.Logging;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MediaManager;

internal partial class Composition
{
    void Setup() => DI.Setup(nameof(Composition))

        // View Models
        .Bind().As(Lifetime.Singleton).To<MainViewViewModel>()
        
        // Main menú
        .Bind<IPageViewModel>(1).As(Lifetime.Singleton).To<MoviesViewModel>()
        .Bind<IPageViewModel>(2).As(Lifetime.Singleton).To<MovieCollectionsViewModel>()
        .Bind<IPageViewModel>(3).As(Lifetime.Singleton).To<TvShowsViewModel>()
        
        // Models

        // Infrastructure
        .Bind<ISchedulerProvider>().As(Lifetime.Singleton).To<SchedulerProvider>()
        .Bind<IConfiguration>().As(Lifetime.Singleton).To(_ => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build())
        
        // Logging
        .Bind<ILoggerFactory>().As(Lifetime.Singleton).To(x =>
        {
            x.Inject<IAppInfo>(out var appInfo);
            x.Inject<LoggingConfiguration>(out var config);
            
            var logFilePath = GetLogFileName(appInfo, config);
            
            var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Default", config.DefaultLogLevel)
                .MinimumLevel.Override("Microsoft", config.MicrosoftLogLevel)
                .WriteTo.File(
                    logFilePath,
                    fileSizeLimitBytes: 10485760,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return new SerilogLoggerFactory(logger);
        })
        
        // Database
        .Bind<IDbContextFactory<MediaManagerDatabaseContext>>().As(Lifetime.Singleton).To<DbContextFactory>()

        .Root<MainViewViewModel>("MainViewViewModel");

    private static string GetLogFileName(IAppInfo appInfo, LoggingConfiguration config) =>
        Path.Combine(appInfo.LogsPath, config.LogFileName);
}
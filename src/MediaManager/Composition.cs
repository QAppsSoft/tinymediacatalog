using System.IO;
using Common;
using Common.Vars;
using Domain;
using Domain.Factories;
using MediaManager.DependencyInjection;
using MediaManager.Infrastructure;
using MediaManager.ViewModels;
using MediaManager.ViewModels.Interfaces;
using MediaManager.ViewModels.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pure.DI;
using Serilog;
using Serilog.Extensions.Logging;
using Services.Abstractions.Domains;
using Services.Abstractions.Media;
using Services.Abstractions.Settings;
using Services.Domains;
using Services.Media;
using Services.Media.Strategy;
using Services.Media.Strategy.Processors;
using Services.Settings;
using Services.Settings.Converters;
using Services.Settings.Models;
using Tools.IO;
using Tools.IO.Kodi;
using Tools.XML;
using Tools.XML.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MediaManager;

internal partial class Composition
{
    void Setup() => DI.Setup(nameof(Composition))

        // View Models
        .Bind().As(Lifetime.Singleton).To<MainViewViewModel>()
        
        // Main menú
        .Bind<IPage>(1).As(Lifetime.Singleton).To<MoviesViewModel>()
        .Bind<IPage>(2).As(Lifetime.Singleton).To<MovieCollectionsViewModel>()
        .Bind<IPage>(3).As(Lifetime.Singleton).To<TvShowsViewModels>()
        .Bind<IPage>(4).As(Lifetime.Singleton).To<SettingsViewModel>()
        
        // General Settings
        .Bind<ISettingsGroup>(1).As(Lifetime.Singleton).To<MovieSettingsViewModel>()
        
        // Models
        .Bind<IAppInfo>().As(Lifetime.Singleton).To<AppInfo>()

        // Infrastructure
        .Bind<ISchedulerProvider>().As(Lifetime.Singleton).To<SchedulerProvider>()
        .Bind<IConfiguration>().As(Lifetime.Singleton).To(_ => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build())
        .Bind<LoggingConfiguration>().As(Lifetime.Singleton).To(x =>
        {
            x.Inject<IConfiguration>(out var configuration);

            return configuration.GetSection(LoggingConfiguration.Logging).Get<LoggingConfiguration>();
        })
        .Bind<IXmlRead>().As(Lifetime.Singleton).To<XmlRead>()
        
        // Settings
        .Bind<ISettingFactory>().As(Lifetime.Singleton).To<SettingFactory>()
        .Bind<ISettingsStore>().As(Lifetime.Singleton).To<FileSettingsStore>()
        .Bind<IConverter<GeneralSettings>>().As(Lifetime.Singleton).To<GeneralSettingsConverter>()
        .Bind<ISetting<TT>>().As(Lifetime.Singleton).To(x =>
        {
            x.Inject<ISettingFactory>(out var settingFactory);
            x.Inject<IConverter<TT>>(out var converter);

            return settingFactory.Create(converter, nameof(TT));
        })
        
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
        .Bind<ILogger<TT>>().As(Lifetime.Transient).To(x =>
        {
            x.Inject<ILoggerFactory>(out var factory);
            return factory.CreateLogger<TT>();
        })
        
        // Database
        .Bind<IDbContextFactory<MediaManagerDatabaseContext>>().As(Lifetime.Singleton).To<DbContextFactory>()

        // Services
        .Bind<MovieContainerManager>().As(Lifetime.Singleton)
        .Bind<IMovieContainerProvider>().As(Lifetime.Singleton).To<MovieContainerManager>()
        .Bind<IMovieContainerManager>().As(Lifetime.Singleton).To<MovieContainerManager>()
        
        // Media Services
        .Bind<IFileStrategy>().To<FileStrategy>()
        .Bind<FileKindSelector>().To<FileKindSelector>()
        .Bind<IFileProcessor<FileKind>>().To<AudioFileProcessor>()
        .Bind<IFileProcessor<FileKind>>().To<ImageFileProcessor>()
        .Bind<IFileProcessor<FileKind>>().To<OtherFileProcessor>()
        .Bind<IFileProcessor<FileKind>>().To<SubtitleProcessor>()
        .Bind<IFileProcessor<FileKind>>().To<VideoFileProcessor>()
        .Bind<IMediaEnumerator>().To<MediaEnumerator>()
        .Bind<IMultimediaLoader>().To<MultimediaLoader>()
        
        // Tools
        .Bind<IOInterface>().To<KodiIO>()
        
        .Root<MainViewViewModel>("MainViewViewModel");

    private static string GetLogFileName(IAppInfo appInfo, LoggingConfiguration config) =>
        Path.Combine(appInfo.LogsPath, config.LogFileName);
}
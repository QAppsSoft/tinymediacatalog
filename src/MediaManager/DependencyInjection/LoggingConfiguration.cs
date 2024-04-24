using Serilog.Events;

namespace MediaManager.DependencyInjection;

public sealed class LoggingConfiguration
{
    public const string Logging = "Logging";
    public string LogFileName { get; init; } = null!;
    public long LimitBytes { get; init; }
    public LogEventLevel DefaultLogLevel { get; init; }
    public LogEventLevel MicrosoftLogLevel { get; init; }
}
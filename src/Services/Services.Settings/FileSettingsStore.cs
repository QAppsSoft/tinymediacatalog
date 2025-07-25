using System.Text.Json;
using Common.Vars;
using Microsoft.Extensions.Logging;
using Services.Abstractions.Settings;

namespace Services.Settings;

public sealed class FileSettingsStore : ISettingsStore
{
    private readonly ILogger<FileSettingsStore> _logger;

    public FileSettingsStore(ILogger<FileSettingsStore> logger, IAppInfo appInfo)
    {
        _logger = logger;
        Location = appInfo.SettingsPath;

        var directory = new DirectoryInfo(Location);
        if (!directory.Exists)
        {
            directory.Create();
        }

        _logger.LogInformation("Settings folder is {Location}", Location);
    }

    private string Location { get; }

    public State Load(string key)
    {
        _logger.LogInformation("Reading setting for {Key}", key);

        var file = Path.Combine(Location, $"{key}.setting");
        var info = new FileInfo(file);

        if (!info.Exists || info.Length == 0)
        {
            return State.Empty;
        }

        var value = File.ReadAllText(file);

        var state = JsonSerializer.Deserialize(value, SourceGenerationContext.Default.State);

        _logger.LogDebug("{Key} has the value {State}", key, state);
        return state;
    }

    public void Save(string key, State state)
    {
        var file = Path.Combine(Location, $"{key}.setting");

        _logger.LogInformation("Creating setting for {Key}", key);

        var fileText = JsonSerializer.Serialize(state, SourceGenerationContext.Default.State);

        _logger.LogInformation("Writing settings for {Key} to {File}", key, file);
        File.WriteAllText(file, fileText);
        _logger.LogInformation("Setting  for {Key} committed", key);
    }
}

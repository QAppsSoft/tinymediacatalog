using Microsoft.Extensions.Logging;
using Services.Abstractions.Settings;

namespace Services.Settings;

public sealed class SettingFactory(ILoggerFactory logFactory, ISettingsStore settingsStore) : ISettingFactory
{
    private readonly ILoggerFactory _logFactory = logFactory ?? throw new ArgumentNullException(nameof(logFactory));
    private readonly ISettingsStore _settingsStore = settingsStore ?? throw new ArgumentNullException(nameof(settingsStore));

    public ISetting<T> Create<T>(IConverter<T> converter, string key) where T : notnull
    {
        //TODO: Cache stored setting and retrieve if required elsewhere
        return new Setting<T>(_logFactory.CreateLogger<T>(), _settingsStore, converter, key);
    }
}
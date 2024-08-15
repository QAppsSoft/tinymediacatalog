using System.Text.Json;
using Services.Abstractions.Settings;
using Services.Settings.Models;

namespace Services.Settings.Converters;

public class GeneralSettingsConverter : IConverter<GeneralSettings>
{
    private const int LastVersion = 1;

    public GeneralSettings Convert(State state)
    {
        if (state == State.Empty)
        {
            return GeneralSettings.Default;
        }

        if (string.IsNullOrWhiteSpace(state.Value))
        {
            throw new SettingsException(SettingsException.Messages.WithParameter(
                SettingsException.Messages.EmptyStateValue,
                typeof(GeneralSettings)));
        }
        
        var settings = JsonSerializer.Deserialize(state.Value, SourceGenerationContext.Default.GeneralSettings);
        
        if (settings is null)
        {
            return GeneralSettings.Default;
        }
        
        return state.Version switch
        {
            1 => settings,
            _ => GeneralSettings.Default
        };
    }

    public State Convert(GeneralSettings state)
    {
        var value = JsonSerializer.Serialize(state, SourceGenerationContext.Default.GeneralSettings);
        return new State(LastVersion, value);
    }

    public GeneralSettings GetDefaultValue() => GeneralSettings.Default;
}
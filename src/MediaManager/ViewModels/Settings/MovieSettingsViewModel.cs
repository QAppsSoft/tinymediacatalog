namespace MediaManager.ViewModels.Settings;

public class MovieSettingsViewModel : ViewModelBase, ISettingsGroup
{
    public string Name => "Películas";
    public string IconKey { get; }
}
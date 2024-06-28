using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels.Settings;

public class SettingsViewModel : ViewModelBase, ISettingsPage
{
    public string Name => "Opciones";
    public string IconKey => "SettingsIcon";

    public SettingsViewModel(ISettingsGroup[] groups)
    {
        Groups = groups;
    }
    
    public ISettingsGroup[] Groups { get; }
}
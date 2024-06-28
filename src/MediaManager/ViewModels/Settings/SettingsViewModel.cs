using MediaManager.ViewModels.Interfaces;
using Services.Abstractions.Settings;
using Services.Settings.Models;

namespace MediaManager.ViewModels.Settings;

public class SettingsViewModel : ViewModelBase, ISettingsPage
{
    public string Name => "Opciones";
    public string IconKey => "SettingsIcon";

    public SettingsViewModel(ISettingsGroup[] groups, ISetting<GeneralSettings> settings)
    {
        Groups = groups;
    }
    
    public ISettingsGroup[] Groups { get; }
}
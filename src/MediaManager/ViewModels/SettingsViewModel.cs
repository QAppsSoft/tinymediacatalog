using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class SettingsViewModel : ViewModelBase, ISettingsPage
{
    public string Name => "Opciones";
    public string IconKey => "SettingsIcon";
}
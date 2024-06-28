using Avalonia.ReactiveUI;
using MediaManager.ViewModels.Settings;

namespace MediaManager.Views.Settings;

public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();
    }
}
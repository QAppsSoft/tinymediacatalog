using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MediaManager.ViewModels.Settings;

namespace MediaManager.Views.Settings;

public partial class MovieSettingsView : ReactiveUserControl<MovieSettingsViewModel>
{
    public MovieSettingsView()
    {
        InitializeComponent();
    }
}
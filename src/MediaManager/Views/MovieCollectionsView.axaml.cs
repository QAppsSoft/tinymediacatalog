using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MediaManager.ViewModels;

namespace MediaManager.Views;

public partial class MovieCollectionsView : ReactiveUserControl<MovieCollectionsViewModel>
{
    public MovieCollectionsView()
    {
        InitializeComponent();
    }
}
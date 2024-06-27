using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MediaManager.ViewModels;

namespace MediaManager.Views;

public partial class MoviesView : ReactiveUserControl<MoviesViewModek>
{
    public MoviesView()
    {
        InitializeComponent();
    }
}
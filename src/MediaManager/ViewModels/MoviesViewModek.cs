using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MoviesViewModek : ViewModelBase, IContentPage
{
    public string Name => "Películas";
    public string IconKey => "MoviesIcon";
}
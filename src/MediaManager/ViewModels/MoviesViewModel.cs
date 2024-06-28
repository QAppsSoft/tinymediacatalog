using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MoviesViewModel : ViewModelBase, IContentPage
{
    public string Name => "Películas";
    public string IconKey => "MoviesIcon";
}
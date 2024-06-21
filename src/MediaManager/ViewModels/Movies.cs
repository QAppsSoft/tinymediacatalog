using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class Movies : ViewModelBase, IContentPage
{
    public string Name => "Películas";
    public string IconKey => "MoviesIcon";
}
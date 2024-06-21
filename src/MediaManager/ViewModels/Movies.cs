using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class Movies : ViewModelBase, IPage
{
    public string Name => "Películas";
    public string IconKey => "MoviesIcon";
}
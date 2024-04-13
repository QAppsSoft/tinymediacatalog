using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MovieCollectionsViewModel : ViewModelBase, IPageViewModel
{
    public string Name => "Colecciones de películas";
    public string IconKey => "MovieCollectionsIcon";
}
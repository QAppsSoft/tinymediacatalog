using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MovieCollections : ViewModelBase, IPage
{
    public string Name => "Colecciones de películas";
    public string IconKey => "MovieCollectionsIcon";
}
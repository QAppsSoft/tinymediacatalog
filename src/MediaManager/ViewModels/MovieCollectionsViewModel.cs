using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MovieCollectionsViewModel : ViewModelBase, IContentPage
{
    public string Name => "Colecciones de películas";
    public string IconKey => "MovieCollectionsIcon";
}
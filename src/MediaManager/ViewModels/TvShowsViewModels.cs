using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class TvShowsViewModels : ViewModelBase, IContentPage
{
    public string Name => "Programas de Televisión";
    public string IconKey => "TvShowsIcon";
}
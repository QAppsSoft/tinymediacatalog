using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class TvShows : ViewModelBase, IPage
{
    public string Name => "Programas de Televisión";
    public string IconKey => "TvShowsIcon";
}
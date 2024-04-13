using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class TvShowsViewModel : ViewModelBase, IPageViewModel
{
    public string Name => "Programas de Televisión";
    public string IconKey => "TvShowsIcon";
}
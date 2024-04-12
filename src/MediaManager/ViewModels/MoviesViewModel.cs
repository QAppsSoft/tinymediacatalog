using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MoviesViewModel : ViewModelBase, IPageViewModel
{
    public string Name => "Movies";
    public string IconKey => "Home";
}
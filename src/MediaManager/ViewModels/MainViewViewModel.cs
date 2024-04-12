using System.Collections.Generic;
using System.Linq;
using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public class MainViewViewModel : ViewModelBase
{
    public MainViewViewModel(IEnumerable<IPageViewModel> pages)
    {
        Pages = pages.ToArray();
        Current = Pages[0];
    }

    public IPageViewModel Current { get; set; }
    
    public IPageViewModel[] Pages { get; set; }
}
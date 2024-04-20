using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MediaManager.ViewModels.Interfaces;

namespace MediaManager.ViewModels;

public partial class MainViewViewModel : ViewModelBase
{
    [ObservableProperty]
    private IPageViewModel _current;

    public MainViewViewModel(IEnumerable<IPageViewModel> pages)
    {
        Pages = pages.ToArray();
        Current = Pages[0];
    }
    
    public IPageViewModel[] Pages { get; set; }
}
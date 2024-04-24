using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MediaManager.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;

namespace MediaManager.ViewModels;

public partial class MainViewViewModel : ViewModelBase
{
    private readonly ILogger _logger;

    [ObservableProperty]
    private IPageViewModel _current;

    public MainViewViewModel(IEnumerable<IPageViewModel> pages, ILogger<MainViewViewModel> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Pages = pages.ToArray();
        Current = Pages[0];
    }
    
    public IPageViewModel[] Pages { get; set; }
}
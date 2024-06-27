using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MediaManager.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;

namespace MediaManager.ViewModels;

public partial class MainViewViewModel : ViewModelBase
{
    private readonly ILogger _logger;

    [ObservableProperty]
    private IPage _current;

    public MainViewViewModel(IPage[] pages, ILogger<MainViewViewModel> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        Pages = pages;
        Current = Pages[0];
    }
    
    public IPage[] Pages { get; }
}
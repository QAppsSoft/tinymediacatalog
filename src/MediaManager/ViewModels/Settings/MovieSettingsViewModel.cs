using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Services.Abstractions.Settings;
using Services.Settings.Models;

namespace MediaManager.ViewModels.Settings;

public partial class MovieSettingsViewModel : ViewModelBase, ISettingsGroup, IActivatableViewModel 
{
    private readonly SourceList<string> _sources = new();
    
    [ObservableProperty]
    private ReadOnlyObservableCollection<string> _paths  = null!; // Will be initialized during activation

    public MovieSettingsViewModel(ISchedulerProvider schedulerProvider, ISetting<GeneralSettings> setting)
    {
        Activator = new ViewModelActivator();

        this.WhenActivated(disposables =>
        {
            // Use WhenActivated to execute logic
            // when the view model gets activated.
            HandleActivation();
            
            // Or use WhenActivated to execute logic
            // when the view model gets deactivated.
            Disposable
                .Create(HandleDeactivation, action => action())
                .DisposeWith(disposables);
        
            _ = _sources.Connect()
                .ObserveOn(schedulerProvider.Dispatcher)
                .Bind(out var paths)
                .Subscribe()
                .DisposeWith(disposables);

            Paths = paths;

            _ = setting.Value
                .Select(settings => settings.MovieSources)
                .ObserveOn(schedulerProvider.Dispatcher)
                .Subscribe(items => _sources.Edit(values =>
                {
                    values.Clear();
                    values.AddRange(items);
                })).DisposeWith(disposables);

            _ = _sources.Connect()
                .ToSortedCollection(SortExpressionComparer<string>.Ascending(x => x))
                .WithLatestFrom(setting.Value)
                .Subscribe(tuple =>
                {
                    var (updatedPaths, generalSettings) = tuple;
                    setting.Write(generalSettings with { MovieSources = updatedPaths.ToArray() });
                }).DisposeWith(disposables);
        });
        
        AddPath = ReactiveCommand.CreateFromTask(GetPathAsync);
        RemovePath = ReactiveCommand.Create<string>(RemoveFromPaths);
    }

    public string Name => "Películas";

    public string IconKey => throw new NotImplementedException();

    public Interaction<Unit, string?> AddPathInteraction { get; } = new();

    public ReactiveCommand<Unit, Unit> AddPath { get; set; }
    public ReactiveCommand<string, Unit> RemovePath { get; set; }

    public ViewModelActivator Activator { get; }

    private async Task GetPathAsync()
    {
        var path = await AddPathInteraction.Handle(Unit.Default);

        if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path) &&
            !_sources.Items.Contains(path, StringComparer.Ordinal))
        {
            _sources.Add(path);
        }
    }

    private void RemoveFromPaths(string path)
    {
        if (!string.IsNullOrWhiteSpace(path) && _sources.Items.Contains(path, StringComparer.Ordinal))
        {
            _sources.Remove(path);
        }
    }

    private void HandleActivation() { }

    private void HandleDeactivation()
    {
        Paths = null!; // Will be initialized during activation
    }
}
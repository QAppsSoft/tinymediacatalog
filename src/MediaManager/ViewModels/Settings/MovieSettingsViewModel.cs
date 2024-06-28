using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Common;
using DynamicData;
using ReactiveUI;

namespace MediaManager.ViewModels.Settings;

public class MovieSettingsViewModel : ViewModelBase, ISettingsGroup
{
    private readonly SourceList<string> _sources = new();
    private readonly CompositeDisposable _cleanup = new();

    public MovieSettingsViewModel(ISchedulerProvider schedulerProvider)
    {
        _sources.Add("1");
        _sources.Add("2");

        _ = _sources.Connect()
            .ObserveOn(schedulerProvider.Dispatcher)
            .Bind(out var paths)
            .Subscribe()
            .DisposeWith(_cleanup);

        Paths = paths;
        
        AddPath = ReactiveCommand.CreateFromTask(GetPathAsync);
        RemovePath = ReactiveCommand.Create<string>(RemoveFromPaths);
    }


    public string Name => "Películas";

    public string IconKey => throw new NotImplementedException();

    public ReadOnlyObservableCollection<string> Paths { get; }

    public Interaction<Unit, string?> AddPathInteraction { get; } = new();

    public ReactiveCommand<Unit, Unit> AddPath { get; set; }
    public ReactiveCommand<string, Unit> RemovePath { get; set; }

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
}
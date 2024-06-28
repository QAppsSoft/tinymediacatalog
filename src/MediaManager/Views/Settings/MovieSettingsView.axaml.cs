using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using MediaManager.ViewModels.Settings;
using ReactiveUI;

namespace MediaManager.Views.Settings;

public partial class MovieSettingsView : ReactiveUserControl<MovieSettingsViewModel>
{
    public MovieSettingsView()
    {
        InitializeComponent();
        
        this.WhenActivated(d =>
        {
            d(ViewModel
                .AddPathInteraction
                .RegisterHandler(
                    async interaction =>
                    {
                        var topLevel = TopLevel.GetTopLevel(this);
                        var options = new FolderPickerOpenOptions()
                            { AllowMultiple = false, Title = "Selecciona la carpeta a agregar" };
                        if (topLevel == null)
                        {
                            return;
                        }

                        var paths = await topLevel.StorageProvider.OpenFolderPickerAsync(options).ConfigureAwait(true);

                        interaction.SetOutput(paths.Count > 0 ? paths[0].Path.LocalPath : null);
                    }));
        });
    }
}
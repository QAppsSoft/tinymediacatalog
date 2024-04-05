using FluentAvalonia.UI.Windowing;

namespace MediaManager.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        TitleBar.ExtendsContentIntoTitleBar = true;
        
        InitializeComponent();
    }
}
namespace Common.Vars;

public sealed class AppInfo : IAppInfo
{
#if DEBUG
    private const bool IsDebug = true;
#else
    private const bool IsDebug = false;
#endif   
    
    private static string DataDirectoryName => "Data";
    private static string BackupDirectoryName => "Backup";
    private static string SettingsDirectoryName => "Settings";
    private static string LogsDirectoryName => "Logs";
    private static string PortableMarkName => ".portable";

    public AppInfo()
    {
        CurrentAppPath = AppContext.BaseDirectory;
        
        PortableFilePath = Path.Combine(CurrentAppPath, PortableMarkName);

        var applicationBaseDataPath = IsPortable
            ? CurrentAppPath
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);

        DataDirectoryPath = Path.Combine(applicationBaseDataPath, DataDirectoryName);

        DatabasePath = IsDebug ? DataBaseFileName : Path.Combine(DataDirectoryPath, DataBaseFileName);

        BackupPath = Path.Combine(applicationBaseDataPath, BackupDirectoryName);
        
        SettingsPath = Path.Combine(DataDirectoryPath, SettingsDirectoryName);
        
        LogsPath = Path.Combine(applicationBaseDataPath, LogsDirectoryName);
    }

    public string AppName => IsDebug ? "MediaManager-Debug" : "MediaManager";
    public string AppFriendlyName => "Media Manager";
    public bool IsPortable => File.Exists(PortableFilePath);
    public string CurrentAppPath { get; }
    public string DataDirectoryPath { get; }
    public string PortableFilePath { get; }
    public string DataBaseFileName => "MediaManager.mmdb";
    public string DatabasePath { get; }
    public string BackupPath { get; }
    public string SettingsPath { get; }
    public string LogsPath { get; }
}
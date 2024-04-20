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

        var applicationBaseDataPath = IsPortable
            ? CurrentAppPath
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
        
        FullDataDirectory = Path.Combine(applicationBaseDataPath, DataDirectoryName);

        PortableFilePath = Path.Combine(CurrentAppPath, PortableMarkName);

        DatabasePath = IsDebug ? DataBaseFileName : Path.Combine(FullDataDirectory, DataBaseFileName);

        BackupPath = Path.Combine(applicationBaseDataPath, BackupDirectoryName);
        
        SettingsPath = Path.Combine(FullDataDirectory, SettingsDirectoryName);
        
        LogsPath = Path.Combine(FullDataDirectory, LogsDirectoryName);
    }

    public string AppName => "MediaManager";
    public string AppFriendlyName => "Media Manager";
    public bool IsPortable => File.Exists(PortableFilePath);
    public string CurrentAppPath { get; }
    public string FullDataDirectory { get; }
    public string PortableFilePath { get; }
    public string DataBaseFileName => IsDebug ? "MediaManager-debug.mmdb" : "MediaManager.mmdb";
    public string DatabasePath { get; }
    public string BackupPath { get; }
    public string SettingsPath { get; }
    public string LogsPath { get; }
}
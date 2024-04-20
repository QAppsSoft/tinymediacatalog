namespace Common.Vars;

public class AppInfo : IAppInfo
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
        
        FullDataDirectory = IsPortable
            ? Path.Combine(CurrentAppPath, DataDirectoryName)
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName, DataDirectoryName);

        PortableFilePath = Path.Combine(CurrentAppPath, PortableMarkName);

        DatabasePath = IsDebug ? DataBaseFileName : Path.Combine(FullDataDirectory, DataBaseFileName);
        
        DatabaseBackupPath = Path.Combine(FullDataDirectory, BackupDirectoryName);
        
        SettingsPath = Path.Combine(FullDataDirectory, SettingsDirectoryName);
        
        LogsPath = Path.Combine(FullDataDirectory, LogsDirectoryName);
    }

    public string AppName => "MediaManager";
    public string AppFriendlyName => "Media Manager";
    public bool IsPortable => File.Exists(PortableFilePath);
    public string CurrentAppPath { get; }
    public string FullDataDirectory { get; }
    public string PortableFilePath { get; }
    public string DataBaseFileName => "MediaManager.mmdb";
    public string DatabasePath { get; }
    public string DatabaseBackupPath { get; }
    public string SettingsPath { get; }
    public string LogsPath { get; }
}
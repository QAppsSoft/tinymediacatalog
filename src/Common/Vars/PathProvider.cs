namespace Common.Vars;

public static class PathProvider
{
    private static readonly bool IsDebug =
#if DEBUG
        true;
#else
        false;
#endif   
    
    private static string DataDirectoryName { get; } = "Data";
    private static string BackupDirectoryName { get; } = "Backup";
    private static string SettingsDirectoryName { get; } = "Settings";
    private static string LogsDirectoryName { get; } = "Logs";
    private static string PortableMarkName { get; } = ".portable";

    public static string CurrentAppPath { get; } = AppContext.BaseDirectory;
    
    public static string DataBaseFileName { get; } = "MediaManager.mmdb";

    public static string FullDataDirectory { get; } = PortableMode.IsPortable
        ? Path.Combine(CurrentAppPath, DataDirectoryName)
        : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppStrings.AppName, DataDirectoryName);

    public static string PortableFilePath { get; } = Path.Combine(CurrentAppPath, PortableMarkName);

    public static string DatabasePath { get; } = IsDebug ?
        DataBaseFileName :
        Path.Combine(FullDataDirectory, DataBaseFileName);
    
    public static string DatabaseBackupPath { get; } = Path.Combine(FullDataDirectory, BackupDirectoryName);
    public static string SettingsPath { get; } = Path.Combine(FullDataDirectory, SettingsDirectoryName);
    public static string LogsPath { get; } = Path.Combine(FullDataDirectory, LogsDirectoryName);
}
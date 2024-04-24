namespace Common.Vars;

public interface IAppInfo
{
    string AppName { get; }
    string AppFriendlyName { get; }
    bool IsPortable { get; }
    string CurrentAppPath { get; }
    string DataDirectoryPath { get; }
    string PortableFilePath { get; }
    string DataBaseFileName { get; }
    string DatabasePath { get; }
    string BackupPath { get; }
    string SettingsPath { get; }
    string LogsPath { get; }
}
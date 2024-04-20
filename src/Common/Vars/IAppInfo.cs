namespace Common.Vars;

public interface IAppInfo
{
    string AppName { get; }
    string AppFriendlyName { get; }
    bool IsPortable { get; }
    string CurrentAppPath { get; }
    string FullDataDirectory { get; }
    string PortableFilePath { get; }
    string DataBaseFileName { get; }
    string DatabasePath { get; }
    string DatabaseBackupPath { get; }
    string SettingsPath { get; }
    string LogsPath { get; }
}
namespace Common.Vars;

public static class AppStrings
{
    public static string AppName { get; } = "MediaManager";
    public static string AppFriendlyName { get; } = "Media Manager";
    public static string ConnectionString { get; } = $"Data Source={PathProvider.DatabasePath};";
}
namespace Common.Vars;

public static class PortableMode
{
    public static bool IsPortable { get; } = File.Exists(PathProvider.PortableFilePath);
}
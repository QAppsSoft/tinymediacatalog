namespace TestsCommons;

public static class Resources
{
    public static string GetResourceFilePath(string fileName) =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
}
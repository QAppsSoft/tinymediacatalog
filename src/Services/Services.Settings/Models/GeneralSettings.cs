namespace Services.Settings.Models;

public record GeneralSettings
{
    public static GeneralSettings Default { get; } = new();
}
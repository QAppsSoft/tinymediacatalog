namespace Services.Settings.Models;

public record GeneralSettings(string[] MovieSources, string[] TvShowSources)
{
    public static GeneralSettings Default { get; } = new([], []);
}
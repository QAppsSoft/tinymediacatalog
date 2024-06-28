namespace Services.Settings.Models;

public record GeneralSettings(string[] MovieSources, string[] TvShowSources)
{
    public static GeneralSettings Default { get; } = new([], []);

    public override int GetHashCode()
    {
        var hashCode = 0;

        foreach (var movieSource in MovieSources)
        {
            hashCode ^= StringComparer.Ordinal.GetHashCode(movieSource);
        }

        foreach (var tvShowSource in TvShowSources)
        {
            hashCode ^= StringComparer.Ordinal.GetHashCode(tvShowSource);
        }

        return hashCode;
    }
}
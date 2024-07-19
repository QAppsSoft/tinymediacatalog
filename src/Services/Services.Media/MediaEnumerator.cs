using Common.Extensions.Enumerable;
using Services.Abstractions.Media;

namespace Services.Media;

public class MediaEnumerator : IMediaEnumerator
{
    // TODO: Reimplement searching for media file with extension filter (setting).
    public IEnumerable<string> GetMovies(string movieContainerPath)
    {
        if (!Directory.Exists(movieContainerPath))
        {
            return Enumerable.Empty<string>();
        }

        var options =  new EnumerationOptions
        {
            IgnoreInaccessible = true,
            MatchCasing = MatchCasing.CaseInsensitive,
            RecurseSubdirectories = true,
        };

        return Directory.EnumerateFiles(movieContainerPath, "movie.nfo", options)
            .Select(file => new FileInfo(file).Directory?.FullName)
            .NotNull();
    }
}
namespace TestsCommons;

public static class Resources
{
    public static string GetResourceFilePath(string fileName) =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);

    public static string KodiMovieNfo => "kodi-movie.nfo";
    
    public static string KodiTvShowNfo => "kodi-tvshow.nfo";
}
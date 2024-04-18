namespace Domain.Models;

public sealed class UniqueId
{
    // anidb
    // imdb
    // moviemeter
    // mpdbtv
    // ofdb
    // omdbapi
    // tmdb
    // trakt
    // tvdb
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    
    public static class ValidNames
    {
        public static string Anidb => "anidb";
        public static string Imdb => "imdb";
        public static string MovieMeter => "moviemeter";
        public static string Mpdbtv => "mpdbtv";
        public static string Ofdb => "ofdb";
        public static string Omdbapi => "omdbapi";
        public static string Tmdb => "tmdb";
        public static string Trakt => "trakt";
        public static string Tvdb => "tvdb";
    }
}
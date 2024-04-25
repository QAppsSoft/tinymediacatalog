using Domain.Models.BaseObjects;

namespace Domain.Models;

public sealed class Rating : GuidEntityBase
{
    // imdb 
    // letterboxd
    // metacrittic
    // myanimelist
    // rogerebert
    // tmdb
    // tomatometerallcritics
    // tomatometeravgcritics
    // trakt
    public string Name { get; set; } = string.Empty;
    public bool Default { get; set; }
    public double Value { get; set; }
    public long Votes { get; set; }
    public int Max { get; set; }

    public static class ValidNames
    {
        public static string Imdb => "imdb";
        public static string LetterBoxd => "letterboxd";
        public static string MetaCrittic => "metacrittic";
        public static string MyAnimeList => "myanimelist";
        public static string RogerEbert => "rogerebert";
        public static string Tmdb => "tmdb";
        public static string TomatoMeterAllCritics => "tomatometerallcritics";
        public static string TomatoMeterAvgCritics => "tomatometeravgcritics";
        public static string Trakt => "trakt";
    }
}
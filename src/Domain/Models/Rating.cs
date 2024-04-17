namespace Domain.Models;

public abstract class Rating
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
    public string Name { get; set; }
    public long Value { get; set; }
    public long Votes { get; set; }
    public int Max { get; set; }
}
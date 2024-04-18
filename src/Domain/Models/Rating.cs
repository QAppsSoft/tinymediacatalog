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
    public long Value { get; set; }
    public long Votes { get; set; }
    public int Max { get; set; }
}
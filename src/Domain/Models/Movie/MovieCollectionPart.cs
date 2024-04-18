using Domain.Models.BaseObjects;

namespace Domain.Models.Movie;

public sealed class MovieCollectionPart : GuidEntityBase
{
    public string Title { get; set; } = string.Empty;
    public string OriginalTitle { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public int TmdbId { get; set; }

    public string PosterPath { get; set; } = string.Empty;
    public string BackdropPath { get; set; } = string.Empty;

    public string Genres { get; set; } = string.Empty;
    
    public DateOnly ReleaseDate { get; set; }

    public string Note { get; set; } = string.Empty;
}
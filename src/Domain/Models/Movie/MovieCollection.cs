using Domain.Models.BaseObjects;

namespace Domain.Models.Movie;

public sealed class MovieCollection : GuidEntityBase
{
    public string Name { get; set; } = string.Empty;
    public int TmdbId { get; set; }
    public string Overview { get; set; } = string.Empty;

    public string PosterPath { get; set; } = string.Empty;
    public string BackdropPath { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;

    public virtual ICollection<MovieCollectionPart> Parts { get; set; } = new List<MovieCollectionPart>();
}
using Domain.Models.BaseObjects;

namespace Domain.Models.Movie;

public abstract class MovieCollectionPart : GuidEntityBase
{
    public string Title { get; set; } = string.Empty;
    public string OriginalTitle { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;

    public string PosterPath { get; set; } = string.Empty;
    public string BackdropPath { get; set; } = string.Empty;

    public IList<ServiceId> ServicesId { get; set; } = new List<ServiceId>();
    public IList<Genre> Genres { get; set; } = new List<Genre>();
    public DateOnly ReleaseDate { get; set; }

    public string Note { get; set; } = string.Empty;
}
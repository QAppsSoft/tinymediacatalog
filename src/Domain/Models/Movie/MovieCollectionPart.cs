using Domain.Models.BaseObjects;

namespace Domain.Models.Movie;

public abstract class MovieCollectionPart : GuidEntityBase
{
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Overview { get; set; }
    
    public string PosterPath { get; set; }
    public string BackdropPath { get; set; }

    public IList<ServiceId> ServicesId { get; set; }
    public IList<Genre> Genres { get; set; }
    public DateOnly ReleaseDate { get; set; }
    
    public string Note { get; set; }
}
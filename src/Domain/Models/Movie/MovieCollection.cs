using Domain.Models.BaseObjects;

namespace Domain.Models.Movie;

public abstract class MovieCollection : GuidEntityBase
{
    public string Name { get; set; }
    public int TmdbId { get; set; }
    public string Overview { get; set; }


    public string PosterPath { get; set; }
    public string BackdropPath { get; set; }
    public string Note { get; set; }
    
    public virtual IList<MovieCollectionPart> Parts { get; set; }
}
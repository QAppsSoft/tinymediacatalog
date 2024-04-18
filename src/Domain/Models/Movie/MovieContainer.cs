using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models.Movie;

public class MovieContainer : GuidEntityBase, ITrackedEntity
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    
    public string NfoPath { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    public string OriginalTitle { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public int Year { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public MovieCollection? Collection { get; set; } = null;
    public string ProductionCompanies { get; set; } = string.Empty;
    public string OriginCountry { get; set; } = string.Empty;
    public string OriginalLanguage { get; set; } = string.Empty;
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public string Note { get; set; } = string.Empty;
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<UniqueId> UniqueIds { get; set; } = new List<UniqueId>();
    public ICollection<Actor> Cast { get; set; } = new List<Actor>();
}
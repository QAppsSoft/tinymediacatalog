using System.ComponentModel.DataAnnotations;
using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models.Movie;

public class MovieContainer : GuidEntityBase, ITrackedEntity
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    
    [StringLength(500)]
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
    public IList<Rating> Ratings { get; set; } = new List<Rating>();
    public string Note { get; set; } = string.Empty;
    public IList<Genre> Genres { get; set; } = new List<Genre>();
    public IList<ServiceId> ServicesId { get; set; } = new List<ServiceId>();
    public IList<Actor> Cast { get; set; } = new List<Actor>();
}
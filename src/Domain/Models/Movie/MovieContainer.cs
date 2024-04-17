using System.ComponentModel.DataAnnotations;
using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models.Movie;

public class MovieContainer : GuidEntityBase, ITrackedEntity
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    
    [StringLength(500)]
    public string NfoPath { get; set; } = null!;
    
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Slogan { get; set; }
    public string Overview { get; set; }
    public int Year { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public MovieCollection? Collection { get; set; }
    public string ProductionCompanies { get; set; }
    public string OriginCountry { get; set; }
    public string OriginalLanguage { get; set; }
    public IList<Rating> Ratings { get; set; }
    public string Note { get; set; }
    public IList<Genre> Genres { get; set; }
    public IList<ServiceId> ServicesId { get; set; }
    public IList<Actor> Cast { get; set; }
}
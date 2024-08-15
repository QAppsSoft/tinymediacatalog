using System.Globalization;
using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;
using Domain.Models.Multimedia;

namespace Domain.Models.Movie;

public partial class MovieContainer : GuidEntityBase, ITrackedEntity
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    
    public string BasePath { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    public string OriginalTitle { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public int Runtime { get; set; }
    public int Year { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public MovieCollection? Collection { get; set; }
    public string ProductionCompanies { get; set; } = string.Empty;
    public string OriginCountry { get; set; } = string.Empty;
    public string OriginalLanguage { get; set; } = string.Empty;
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public string Note { get; set; } = string.Empty;
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<UniqueId> UniqueIds { get; set; } = new List<UniqueId>();
    public ICollection<Actor> Cast { get; set; } = new List<Actor>();
    
    public ICollection<MultimediaFile> Files { get; set; } = new List<MultimediaFile>();
    public DateTime DateAdded { get; set; }
}

// Methods
public partial class MovieContainer
{
    public MovieContainer SetTitle(string title)
    {
        Title = title;
        return this;
    }
    
    public MovieContainer SetOriginalTitle(string originalTitle)
    {
        OriginalTitle = originalTitle;
        return this;
    }

    public MovieContainer SetYear(int year)
    {
        Year = year;
        return this;
    }
    
    public MovieContainer SetSlogan(string slogan)
    {
        Slogan = slogan;
        return this;
    }
    
    public MovieContainer SetRuntime(int runtime)
    {
        Runtime = runtime;
        return this;
    }
    
    public MovieContainer SetReleaseDate(DateOnly releaseDate)
    {
        ReleaseDate = releaseDate;
        return this;
    }
    
    public MovieContainer SetReleaseDate(string releaseDate)
    {
        if (DateOnly.TryParse(releaseDate, CultureInfo.InvariantCulture, out var date))
        {
            SetReleaseDate(date);
        }
        
        return this;
    }
    
    public MovieContainer SetOriginalLanguage(string originalLanguage)
    {
        OriginalLanguage = originalLanguage;
        return this;
    }
    
    public MovieContainer SetDateAdded(DateTime dateAdded)
    {
        DateAdded = dateAdded;
        return this;
    }
    
    public MovieContainer SetDateAdded(string dateAdded)
    {
        if (DateTime.TryParse(dateAdded, CultureInfo.InvariantCulture, out var dateTime))
        {
            SetDateAdded(dateTime);
        }

        return this;
    }
}
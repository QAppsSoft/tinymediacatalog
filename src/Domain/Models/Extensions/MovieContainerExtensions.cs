using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Domain.Models.Movie;

public static class MovieContainerExtensions
{
    public static MovieContainer SetTitle(this MovieContainer movie, string title)
    {
        movie.Title = title;
        return movie;
    }
    
    public static MovieContainer SetOriginalTitle(this MovieContainer movie, string originalTitle)
    {
        movie.OriginalTitle = originalTitle;
        return movie;
    }

    public static MovieContainer SetYear(this MovieContainer movie, int year)
    {
        movie.Year = year;
        return movie;
    }
    
    public static MovieContainer SetSlogan(this MovieContainer movie, string slogan)
    {
        movie.Slogan = slogan;
        return movie;
    }
    
    public static MovieContainer SetRuntime(this MovieContainer movie, int runtime)
    {
        movie.Runtime = runtime;
        return movie;
    }
    
    public static MovieContainer SetReleaseDate(this MovieContainer movie, DateOnly releaseDate)
    {
        movie.ReleaseDate = releaseDate;
        return movie;
    }
    
    public static MovieContainer SetReleaseDate(this MovieContainer movie, string releaseDate)
    {
        if (DateOnly.TryParse(releaseDate, CultureInfo.InvariantCulture, out var date))
        {
            movie.SetReleaseDate(date);
        }
        
        return movie;
    }
    
    public static MovieContainer SetOriginalLanguage(this MovieContainer movie, string originalLanguage)
    {
        movie.OriginalLanguage = originalLanguage;
        return movie;
    }
    
    public static MovieContainer SetDateAdded(this MovieContainer movie, DateTime dateAdded)
    {
        movie.DateAdded = dateAdded;
        return movie;
    }
    
    public static MovieContainer SetDateAdded(this MovieContainer movie, string dateAdded)
    {
        if (DateTime.TryParse(dateAdded, CultureInfo.InvariantCulture, out var dateTime))
        {
            movie.SetDateAdded(dateTime);
        }

        return movie;
    }
}
using System.Globalization;
using Domain;
using Domain.Models;
using Domain.Models.Movie;
using Microsoft.EntityFrameworkCore;
using Tools.Enums;
using Tools.XML.Interfaces;
using Movie = Tools.IO.Kodi.Models.Movie;

namespace Tools.IO.Kodi;

public class KodiIO(IXmlRead xmlRead, IDbContextFactory<MediaManagerDatabaseContext> databaseContextFactory) : IOInterface
{
    private readonly IXmlRead _xmlRead = xmlRead ?? throw new ArgumentNullException(nameof(xmlRead));
    private readonly IDbContextFactory<MediaManagerDatabaseContext> _databaseContextFactory = databaseContextFactory ?? throw new ArgumentNullException(nameof(databaseContextFactory));
    
    public bool ShowInSettings { get; set; } = true;
    public NfoType Type { get; set; } = NfoType.KODI;
    public string IOHandlerName { get; set; } = "KODI";
    public string IOHandlerDescription { get; set; } = "IO handler for KODI";
    public Uri IOHandlerUri { get; set; } = new("https://kodi.tv/");

    public void LoadMovie(MovieContainer movieModel)
    {
        Movie? movie;

        try
        {
            movie = _xmlRead.ParseXml<Movie>(movieModel.NfoPath);
        }
        catch (InvalidOperationException _)
        {
            return;
        }
            
        if (movie is null)
        {
            return;
        }
        
        // Title
        movieModel.Title = movie.Title;
        
        // Original title
        movieModel.OriginalTitle = movie.OriginalTitle;
        
        // Year
        movieModel.Year = movie.Year;
        
        // Ratings
        movieModel.Ratings = movie.RatingsContainer.Rating.ConvertAll(rating => new Rating
        {
            Default = rating.Default,
            Max = rating.Max,
            Name = rating.Name,
            Value = rating.Value,
            Votes = rating.Votes,
        });
        
        // Sets
        
        // Slogan
        movieModel.Slogan = movie.TagLine;
        
        // Runtime
        movieModel.Runtime = movie.Runtime;
        
        // Thumb
        
        // Mpaa
        //movieModel.Mpaa = movie.Mpaa;
        
        // Certification
        //movieModel.Certification = movie.Certification;
        
        // UniqueIds
        movieModel.UniqueIds = new List<UniqueId>
        {
            new() { Id = movie.Id, Name = UniqueId.ValidNames.Imdb },
            new() { Id = movie.Tmdbid, Name = UniqueId.ValidNames.Tmdb },
        };
        
        // Uniqueids
        
        // Countries
        
        // Premiered
        movieModel.ReleaseDate = DateOnly.Parse(movie.Premiered, CultureInfo.InvariantCulture);
        
        // Watched
        
        // Play count
        
        // Genres
        
        // Studios
        
        // Tags
        
        // Cast (actors)
        movieModel.Cast.Clear();
        movieModel.Cast = movie.Cast.ConvertAll(castMember => GetActor(movieModel, castMember));
        
        // Trailer
        
        // Languages (original)
        movieModel.OriginalLanguage = movie.Languages;
        
        // Date added
        movieModel.DateAdded = DateTime.Parse(movie.DateAdded, CultureInfo.InvariantCulture);

        // Fileinfo
    }

    public void SaveMovie(MovieContainer movieModel)
    {
        throw new NotImplementedException();
    }

    private Actor GetActor(MovieContainer movieModel, Models.Actor castMember)
    {
        var person = GetPerson(castMember);
        var actor = new Actor
            { Movie = movieModel, MovieContainerId = movieModel.Id, Person = person, PersonId = person.Id };
        return actor;
    }

    private Person GetPerson(Tools.IO.Kodi.Models.Actor actor)
    {
        using var context = _databaseContextFactory.CreateDbContext();

        var person = context.Persons
            .FirstOrDefault(x =>
                x.UniqueIds.Any(y =>
                    y.Name == UniqueId.ValidNames.Tmdb &&
                    y.Id == actor.Tmdbid.ToString(CultureInfo.InvariantCulture)));

        return person ?? new Person
        {
            Name = actor.Name,
            Profile = actor.Profile,
            Thumb = actor.Thumb,
            UniqueIds = new List<UniqueId>
            {
                new()
                {
                    Id = actor.Tmdbid.ToString(CultureInfo.InvariantCulture),
                    Name = actor.Name,
                },
            },
        };
    }
}
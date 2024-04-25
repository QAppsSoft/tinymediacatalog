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

    public async Task LoadMovieAsync(MovieContainer movieModel)
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
        movieModel.Ratings.Clear();
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
        movieModel.UniqueIds.Clear();
        movieModel.UniqueIds = movie.UniqueIds.ConvertAll(value => new UniqueId { Name = value.Type, Id = value.Text });
        
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
        foreach (var castMember in movie.Cast)
        {
            var actor = await GetActorAsync(movieModel, castMember).ConfigureAwait(false);
            movieModel.Cast.Add(actor);
        }
        
        // Trailer
        
        // Languages (original)
        movieModel.OriginalLanguage = movie.Languages;
        
        // Date added
        movieModel.DateAdded = DateTime.Parse(movie.DateAdded, CultureInfo.InvariantCulture);

        // Fileinfo
    }

    public Task SaveMovieAsync(MovieContainer movieModel)
    {
        throw new NotImplementedException();
    }

    private async Task<Actor> GetActorAsync(MovieContainer movieModel, Models.Actor castMember)
    {
        var person = await GetPersonAsync(castMember).ConfigureAwait(false);
        var actor = new Actor
        {
            Movie = movieModel, MovieContainerId = movieModel.Id, Person = person, PersonId = person.Id,
            Role = castMember.Role,
        };
        
        return actor;
    }

    private async Task<Person> GetPersonAsync(Tools.IO.Kodi.Models.Actor actor)
    {
        var context = await _databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var person = await context.Persons
            .FirstOrDefaultAsync(x =>
                x.UniqueIds.Any(y =>
                    y.Name == UniqueId.ValidNames.Tmdb &&
                    y.Id == actor.Tmdbid.ToString(CultureInfo.InvariantCulture)))
            .ConfigureAwait(false);

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
                        Name = UniqueId.ValidNames.Tmdb,
                    },
                },
            };
        }
    }
}
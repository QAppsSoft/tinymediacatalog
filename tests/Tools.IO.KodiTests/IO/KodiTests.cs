using System.Globalization;
using Domain.Models;
using Domain.Models.Movie;
using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Services.Domains;
using TestsCommons;
using TestsCommons.Domain;
using Tools.IO.Kodi;
using Tools.XML;
using Tools.XML.Interfaces;

namespace Tools.IO.KodiTests.IO;

[TestFixture]
public class KodiTests
{
    private readonly IXmlRead _xmlRead = new XmlRead();
    private DbContextFactoryFixture _factory;
    private MovieContainerManager _movieContainerManager;

    [SetUp]
    public async Task OnStartAsync()
    {
        _factory = new DbContextFactoryFixture();
        _movieContainerManager = new MovieContainerManager(_factory);
        await using var db = _factory.CreateDbContext();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
    }
    
    [Test]
    public async Task LoadMovie_NFO_with_Kodi_should_return_correct_basic_data_Async()
    {
        var mc = await InitializeMovieContainerAsync(_factory);
        var kodi = new KodiIO(_xmlRead, _movieContainerManager,_movieContainerManager);
    
        await kodi.LoadMovieAsync(mc.Id);
        var updated = await GetUpdatedMovieContainerAsync(mc.Id);
        
        // Assert
        updated.Title.Should().Be("A todo gas");
        updated.OriginalTitle.Should().Be("The Fast and the Furious");
        updated.Year.Should().Be(2001);
        updated.Slogan.Should().Be("Cuando el sol se oculta, otro mundo cobra vida.");
        updated.Runtime.Should().Be(109);
        updated.UniqueIds.First(x => x.Name == UniqueId.ValidNames.Imdb).Id.Should().Be("tt0232500");
        updated.UniqueIds.First(x => x.Name == UniqueId.ValidNames.Tmdb).Id.Should().Be("9799");
        updated.ReleaseDate.Should().Be(DateOnly.Parse("2001-10-05", CultureInfo.InvariantCulture));
        updated.DateAdded.Should().Be(DateTime.Parse("2024-03-09 06:14:29", CultureInfo.InvariantCulture));
    }

    [Test]
    public async Task LoadMovie_NFO_with_Kodi_should_return_correct_cast_data_Async()
    {
        var mc = await InitializeMovieContainerAsync(_factory);
        var kodi = new KodiIO(_xmlRead, _movieContainerManager,_movieContainerManager);
    
        await kodi.LoadMovieAsync(mc.Id);
        var updated = await GetUpdatedMovieContainerAsync(mc.Id);
        var firstMember = updated.Cast.OrderBy(x => x.Order).First();
        
        // Assert
        updated.Cast.Count.Should().NotBe(0);
        firstMember.Role.Should().Be("Brian O'Conner");
        firstMember.Person.Name.Should().Be("Paul Walker");
        firstMember.Person.Thumb.Should().Be("https://image.tmdb.org/t/p/h632/q2PLqKHYCs35HR7QybaNPH3JT96.jpg");
        firstMember.Person.Profile.Should().Be("https://www.themoviedb.org/person/8167");
        
        firstMember.Person.UniqueIds.Should().BeEquivalentTo(new List<UniqueId>
        {
            new() { Id = "8167", Name = UniqueId.ValidNames.Tmdb },
        });
    }
    
    [Test]
    public async Task LoadMovie_NFO_with_Kodi_should_return_correct_languages_data_Async()
    {
        var mc = await InitializeMovieContainerAsync(_factory);
        var kodi = new KodiIO(_xmlRead, _movieContainerManager,_movieContainerManager);
    
        await kodi.LoadMovieAsync(mc.Id);
        var updated = await GetUpdatedMovieContainerAsync(mc.Id);
        
        // Assert
        updated.OriginalLanguage.Should().Be("inglés,español");
    }
    
    [Test]
    public async Task Empty_NFO_file_should_not_update_MovieModel_Async()
    {
        const string defaultTitle = "Test title";
        using var storage = new StorageFixture();
        var emptyFile = storage.GetEmptyFile(".nfo");
        var mc = await InitializeEmptyNfoMovieContainerAsync(_factory, emptyFile, defaultTitle);
        var kodi = new KodiIO(_xmlRead, _movieContainerManager,_movieContainerManager);

        await kodi.LoadMovieAsync(mc.Id);
        var updated = await GetUpdatedMovieContainerAsync(mc.Id);

        // Assert
        updated.Title.Should().Be(defaultTitle);
    }

    private async Task<MovieContainer> GetUpdatedMovieContainerAsync(Guid id)
    {
        await using var context = _factory.CreateDbContext();

        return await context.Movies
            .Include(x => x.UniqueIds)
            .Include(x => x.Cast).ThenInclude(x => x.Person).ThenInclude(x => x.UniqueIds)
            .Include(x => x.Ratings)
            .FirstAsync(x => x.Id == id)
            .ConfigureAwait(false);
    }

    private static async Task<MovieContainer> InitializeEmptyNfoMovieContainerAsync(DbContextFactoryFixture factory, string emptyFile, string defaultTitle)
    {
        var db = factory.CreateDbContext();

        var mc = new MovieContainer
        {
            Title = defaultTitle,
            Files = new List<MultimediaFile>
            {
                new NfoFile { FilePath = emptyFile },
            }
        };

        await db.AddAsync(mc);
        await db.SaveChangesAsync();

        return mc;
    }
    
    private static async Task<MovieContainer> InitializeMovieContainerAsync(DbContextFactoryFixture factory)
    {
        var db = factory.CreateDbContext();

        var mc = new MovieContainer
        {
            Files = new List<MultimediaFile>
            {
                new NfoFile { FilePath = Resources.GetResourceFilePath(Resources.KodiMovieNfo) },
            }
        };

        await db.AddAsync(mc);
        await db.SaveChangesAsync();

        return mc;
    }
}
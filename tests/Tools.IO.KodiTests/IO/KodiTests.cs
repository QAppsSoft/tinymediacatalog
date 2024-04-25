using System.Globalization;
using Domain.Models;
using Domain.Models.Movie;
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
    private MovieContainer _movieModel;
    
    [SetUp]
    public void OnStart()
    {
        _factory = new DbContextFactoryFixture();
        _movieModel = new MovieContainer { NfoPath = Resources.GetResourceFilePath(Resources.KodiMovieNfo) };
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
    }
    
    [Test]
    public void LoadMovie_NFO_with_Kodi_should_return_correct_basic_data()
    {
        var kodi = new KodiIO(_xmlRead, _factory);

        kodi.LoadMovie(_movieModel);
        
        // Assert
        _movieModel.Title.Should().Be("A todo gas");
        _movieModel.OriginalTitle.Should().Be("The Fast and the Furious");
        _movieModel.Year.Should().Be(2001);

        _movieModel.Slogan.Should().Be("Cuando el sol se oculta, otro mundo cobra vida.");
        _movieModel.Runtime.Should().Be(109);

        _movieModel.UniqueIds.First(x=>x.Name==UniqueId.ValidNames.Imdb).Id.Should().Be("tt0232500");
        _movieModel.UniqueIds.First(x => x.Name == UniqueId.ValidNames.Tmdb).Id.Should().Be("9799");
        _movieModel.ReleaseDate.Should().Be(DateOnly.Parse("2001-10-05", CultureInfo.InvariantCulture));
        _movieModel.DateAdded.Should().Be(DateTime.Parse("2024-03-09 06:14:29", CultureInfo.InvariantCulture));
    }

    [Test]
    public void LoadMovie_NFO_with_Kodi_should_return_correct_cast_data()
    {
        var kodi = new KodiIO(_xmlRead, _factory);

        kodi.LoadMovie(_movieModel);
        
        _movieModel.Cast.Count.Should().NotBe(0);
        _movieModel.Cast.First().Role.Should().Be("Brian O'Conner");

        _movieModel.Cast.First().Person.Name.Should().Be("Paul Walker");
        _movieModel.Cast.First().Person.Thumb.Should().Be("https://image.tmdb.org/t/p/h632/q2PLqKHYCs35HR7QybaNPH3JT96.jpg");
        _movieModel.Cast.First().Person.Profile.Should().Be("https://www.themoviedb.org/person/8167");
        
        _movieModel.Cast.First().Person.UniqueIds.Should().BeEquivalentTo(new List<UniqueId>
        {
            new() { Id = "8167", Name = UniqueId.ValidNames.Tmdb },
        });
    }
    
    [Test]
    public void LoadMovie_NFO_with_Kodi_should_return_correct_languages_data()
    {
        var kodi = new KodiIO(_xmlRead, _factory);

        kodi.LoadMovie(_movieModel);

        _movieModel.OriginalLanguage.Should().Be("inglés,español");
    }
    
    [Test]
    public void Empty_NFO_file_should_not_update_MovieModel()
    {
        using var storage = new StorageFixture();
        var emptyFile = storage.GetEmptyFile(".nfo");
        _movieModel.NfoPath = emptyFile;
        _movieModel.Title = "Test title";
        var kodi = new KodiIO(_xmlRead, _factory);

        kodi.LoadMovie(_movieModel);

        _movieModel.Title.Should().Be("Test title");
    }
}
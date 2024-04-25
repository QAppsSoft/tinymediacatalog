using System.Globalization;
using Models.Common;
using Models.MovieModels;
using TestsCommons;
using Tools.IO.Kodi;

namespace ToolsTests.IO;

[TestFixture]
public class KodiTests
{
    [Test]
    public void LoadMovie_NFO_with_Kodi_should_return_correct_basic_data()
    {
        var movieModel = new MovieModel { NfoPathOnDisk = Resources.GetResourceFilePath(Resources.KodiMovieNfo) };
        var kodi = new KodiIO();

        kodi.LoadMovie(movieModel);
        
        // Assert
        movieModel.Title.Should().Be("A todo gas");
        movieModel.OriginalTitle.Should().Be("The Fast and the Furious");
        movieModel.Year.Should().Be(2001);

        movieModel.Plot.Should().Be(
            "Una misteriosa banda de delincuentes se dedica a robar camiones en plena marcha desde vehículos deportivos. La policía decide infiltrar a un hombre en el mundo de las carreras ilegales para descubrir posibles sospechosos. El joven y apuesto Brian entra en el mundo del tuning donde conoce a Dominic, rey indiscutible de este mundo y sospechoso número uno, pero todo se complica cuando se enamora de la hermana de éste.");
        movieModel.Outline.Should().Be(
            "Una misteriosa banda de delincuentes se dedica a robar camiones en plena marcha desde vehículos deportivos. La policía decide infiltrar a un hombre en el mundo de las carreras ilegales para descubrir posibles sospechosos. El joven y apuesto Brian entra en el mundo del tuning donde conoce a Dominic, rey indiscutible de este mundo y sospechoso número uno, pero todo se complica cuando se enamora de la hermana de éste.");
        movieModel.Tagline.Should().Be("Cuando el sol se oculta, otro mundo cobra vida.");
        movieModel.Runtime.Should().Be(109);

        movieModel.Mpaa.Should().Be("12");
        movieModel.Certification.Should().Be("12");
        movieModel.Id.Should().Be("tt0232500");
        movieModel.Tmdbid.Should().Be(9799);

        movieModel.Premiered.Should().Be(DateTime.Parse("2001-10-05", CultureInfo.InvariantCulture));
        movieModel.DateAdded.Should().Be(DateTime.Parse("2024-03-09 06:14:29", CultureInfo.InvariantCulture));
    }

    [Test]
    public void LoadMovie_NFO_with_Kodi_should_return_correct_cast_data()
    {
        var movieModel = new MovieModel { NfoPathOnDisk = Resources.GetResourceFilePath(Resources.KodiMovieNfo) };
        var kodi = new KodiIO();

        kodi.LoadMovie(movieModel);
        
        movieModel.Cast.Count.Should().NotBe(0);
        movieModel.Cast[0].Should().BeEquivalentTo(new ActorModel
        {
            Name = "Paul Walker",
            Role = "Brian O'Conner",
            Thumb = "https://image.tmdb.org/t/p/h632/q2PLqKHYCs35HR7QybaNPH3JT96.jpg",
            Profile = "https://www.themoviedb.org/person/8167",
            TmdbId = 8167,
        });
    }
    
    [Test]
    public void LoadMovie_NFO_with_Kodi_should_return_correct_languages_data()
    {
        var movieModel = new MovieModel { NfoPathOnDisk = Resources.GetResourceFilePath(Resources.KodiMovieNfo) };
        var kodi = new KodiIO();

        kodi.LoadMovie(movieModel);
        
        movieModel.Languages.Count.Should().Be(2);
        movieModel.Languages.Should().BeEquivalentTo("inglés", "español");
    }
    
    [Test]
    public void Empty_NFO_file_should_not_update_MovieModel()
    {
        using var storage = new StorageFixture();
        var emptyFile = storage.GetEmptyFile(".nfo");
        var movieModel = new MovieModel { NfoPathOnDisk = emptyFile, Title = "Test title"};
        var kodi = new KodiIO();

        kodi.LoadMovie(movieModel);

        movieModel.Title.Should().Be("Test title");
    }
}
using System.Globalization;
using Models.Common;
using Models.MovieModels;
using Tools.Enums;
using Tools.IO.Kodi.Models;
using Tools.XML.Interfaces;

namespace Tools.IO.Kodi;

public class KodiIO(IXmlRead xmlRead) : IOInterface
{
    private readonly IXmlRead _xmlRead = xmlRead ?? throw new ArgumentNullException(nameof(xmlRead));
    public bool ShowInSettings { get; set; } = true;
    public NfoType Type { get; set; } = NfoType.KODI;
    public string IOHandlerName { get; set; } = "KODI";
    public string IOHandlerDescription { get; set; } = "IO handler for KODI";
    public Uri IOHandlerUri { get; set; } = new("https://kodi.tv/");

    public void LoadMovie(MovieModel movieModel)
    {

        Movie? movie;

        try
        {
            movie = _xmlRead.ParseXml<Movie>(movieModel.NfoPathOnDisk);
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
        movieModel.Ratings = movie.RatingsContainer.Rating.ConvertAll(rating => new RatingModel
        {
            Default = rating.Default,
            Max = rating.Max,
            Name = rating.Name,
            Value = rating.Value,
            Votes = rating.Votes,
        });
        
        // Sets
        
        
        // Plot
        movieModel.Plot = movie.Plot;
        
        // Outline
        movieModel.Outline = movie.Outline;
        
        // Tagline
        movieModel.Tagline = movie.TagLine;
        
        // Runtime
        movieModel.Runtime = movie.Runtime;
        
        // Thumb
        
        
        // Mpaa
        movieModel.Mpaa = movie.Mpaa;
        
        // Certification
        movieModel.Certification = movie.Certification;
        
        // Id
        movieModel.Id = movie.Id;
        
        // TmdbId
        movieModel.Tmdbid = movie.Tmdbid;
        
        // Uniqueids
        
        // Countries
        
        // Premiered
        movieModel.Premiered = DateTime.Parse(movie.Premiered, CultureInfo.InvariantCulture);
        
        // Watched
        
        // Play count
        
        // Genres
        
        // Studios
        
        // Tags
        
        // Cast (actors)
        movieModel.Cast = movie.Cast.ConvertAll(cast => new ActorModel
        {
            Name = cast.Name,
            Profile = cast.Profile,
            Role = cast.Role,
            Thumb = cast.Thumb,
            TmdbId = cast.Tmdbid,
        });
        
        // Trailer
        
        // Languages (original)
        movieModel.Languages = movie.Languages.Split(',').ToList();
        
        // Date added
        movieModel.DateAdded = DateTime.Parse(movie.DateAdded, CultureInfo.InvariantCulture);

        // Fileinfo
    }

    public void SaveMovie(MovieModel movieModel)
    {
        throw new NotImplementedException();
    }
}
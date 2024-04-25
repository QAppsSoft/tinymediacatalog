using System.Globalization;
using System.Xml;
using Models.Common;
using Models.MovieModels;
using Tools.Enums;
using Tools.IO.Kodi.Models;
using Tools.XML;

namespace Tools.IO.Kodi;

public class KodiIO : IOInterface
{
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
            movie = XRead.ParseXml<Movie>(movieModel.NfoPathOnDisk);
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

    private static List<ActorModel> GetCast(XmlDocument xmlReader) =>
        xmlReader.GetElementsByTagName("actor").Cast<XmlElement>()
            .Select(node =>
            {
                var document = XRead.OpenXml("<x>" + node.InnerXml + "</x>");

                var name = XRead.GetString(document, "name");
                var role = XRead.GetString(document, "role");
                var thumb = XRead.GetString(document, "thumb");
                var profile = XRead.GetString(document, "profile");
                var tmdbid = XRead.GetInt(document, "tmdbid");

                return new ActorModel
                {
                    Name = name,
                    Role = role,
                    Thumb = thumb,
                    Profile = profile,
                    TmdbId = tmdbid,
                };
            }).ToList();
}
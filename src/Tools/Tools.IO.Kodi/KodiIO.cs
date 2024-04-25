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
        movieModel.OriginalTitle = movie.Originaltitle;
        
        // Year
        movieModel.Year = movie.Year;
        
        // Ratings
        movieModel.Ratings = movie.Ratings.Rating.ConvertAll(rating => new RatingModel
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
        movieModel.Tagline = movie.Tagline;
        
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
        movieModel.Cast = movie.Actor.ConvertAll(cast => new ActorModel
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
        movieModel.DateAdded = DateTime.Parse(movie.Dateadded, CultureInfo.InvariantCulture);

        // Fileinfo


        // var xmlReader = XRead.OpenPath(movieModel.NfoPathOnDisk);
        //
        // if (xmlReader == null)
        // {
        //     return;
        // }
        //
        // // Title
        // movieModel.Title = XRead.GetString(xmlReader, "title");
        //
        // // Original title
        // movieModel.OriginalTitle = XRead.GetString(xmlReader, "originaltitle");
        //
        // // Year
        // movieModel.Year = XRead.GetInt(xmlReader, "year");
        //
        // // Ratings
        // movieModel.Ratings = XRead.GetRatings(xmlReader);
        //
        // // Sets
        //
        //
        // // Plot
        // movieModel.Plot = XRead.GetString(xmlReader, "plot");
        //
        // // Outline
        // movieModel.Outline = XRead.GetString(xmlReader, "outline");
        //
        // // Tagline
        // movieModel.Tagline = XRead.GetString(xmlReader, "tagline");
        //
        // // Runtime
        // movieModel.Runtime = XRead.GetInt(xmlReader, "runtime");
        //
        // // Thumb
        //
        //
        // // Mpaa
        // movieModel.Mpaa = XRead.GetString(xmlReader, "mpaa");
        //
        // // Certification
        // movieModel.Certification = XRead.GetString(xmlReader, "certification");
        //
        // // Id
        // movieModel.Id = XRead.GetString(xmlReader, "id");
        //
        // // TmdbId
        // movieModel.Tmdbid = XRead.GetInt(xmlReader, "tmdbid");
        //
        // // Uniqueids
        //
        // // Countries
        //
        // // Premiered
        // movieModel.Premiered = XRead.GetDateTime(xmlReader, "premiered");
        //
        // // Watched
        //
        // // Play count
        //
        // // Genres
        //
        // // Studios
        //
        // // Tags
        //
        // // Cast (actors)
        // movieModel.Cast = GetCast(xmlReader);
        //
        // // Trailer
        //
        // // Languages (original)
        // movieModel.Languages = XRead.GetString(xmlReader, "languages").Split(',').ToList();
        //
        // // Date added
        // movieModel.DateAdded = XRead.GetDateTime(xmlReader, "dateadded");
        //
        // // Fileinfo
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
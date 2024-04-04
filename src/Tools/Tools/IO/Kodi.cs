using System.Xml;
using Models.Common;
using Models.MovieModels;
using Tools.Enums;
using Tools.XML;

namespace Tools.IO;

public class Kodi : IOInterface
{
    public bool ShowInSettings { get; set; } = true;
    public NfoType Type { get; set; } = NfoType.KODI;
    public string IOHandlerName { get; set; } = "KODI";
    public string IOHandlerDescription { get; set; } = "IO handler for KODI";
    public Uri IOHandlerUri { get; set; } = new("https://kodi.tv/");
    
    public void LoadMovie(MovieModel movieModel)
    {
        var xmlReader = XRead.OpenPath(movieModel.NfoPathOnDisk);

        if (xmlReader == null)
        {
            return;
        }

        // Title
        movieModel.Title = XRead.GetString(xmlReader, "title");
        
        // Original title
        movieModel.OriginalTitle = XRead.GetString(xmlReader, "originaltitle");
        
        // Year
        movieModel.Year = XRead.GetInt(xmlReader, "year");
        
        // Ratings
        movieModel.Ratings = XRead.GetRatings(xmlReader);

        // Sets
        
        
        // Plot
        movieModel.Plot = XRead.GetString(xmlReader, "plot");
        
        // Outline
        movieModel.Outline = XRead.GetString(xmlReader, "outline");
        
        // Tagline
        movieModel.Tagline = XRead.GetString(xmlReader, "tagline");
        
        // Runtime
        movieModel.Runtime = XRead.GetInt(xmlReader, "runtime");
        
        // Thumb
        
        
        // Mpaa
        movieModel.Mpaa = XRead.GetString(xmlReader, "mpaa");
        
        // Certification
        movieModel.Certification = XRead.GetString(xmlReader, "certification");
        
        // Id
        movieModel.Id = XRead.GetString(xmlReader, "id");
        
        // TmdbId
        movieModel.Tmdbid = XRead.GetInt(xmlReader, "tmdbid");
        
        // Uniqueids
        
        // Countries
        
        // Premiered
        movieModel.Premiered = XRead.GetDateTime(xmlReader, "premiered");
        
        // Watched
        
        // Play count
        
        // Genres
        
        // Studios
        
        // Tags
        
        // Cast (actors)
        movieModel.Cast = GetCast(xmlReader);
        
        // Trailer
        
        // Languages (original)
        movieModel.Languages = XRead.GetString(xmlReader, "languages").Split(',').ToList();
        
        // Date added
        movieModel.DateAdded = XRead.GetDateTime(xmlReader, "dateadded");
        
        // Fileinfo
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
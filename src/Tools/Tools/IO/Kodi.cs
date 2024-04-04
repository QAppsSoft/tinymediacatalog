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
        
        // Trailer
        
        // Languages (original)
        movieModel.Languages = XRead.GetString(xmlReader, "languages");
        
        // Date added
        movieModel.DateAdded = XRead.GetDateTime(xmlReader, "dateadded");
        
        // Fileinfo
    }
}
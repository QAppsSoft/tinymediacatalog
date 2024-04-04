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
        
        
        
        throw new NotImplementedException();
    }
}
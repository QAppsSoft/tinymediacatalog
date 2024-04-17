using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models.Multimedia;

public abstract class MultimediaFile : GuidEntityBase, ITrackedEntity
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public abstract Kind Kind { get; set; }
    
    public long Size { get; set; }
}

public enum Kind
{
    Video,
    Nfo,
    Image,
    Audio,
    Subtitle,
}
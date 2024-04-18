using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models.Multimedia;

public abstract class MultimediaFile : GuidEntityBase, ITrackedEntity
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public abstract Kind Kind { get; }
    
    public long Size { get; set; }
}
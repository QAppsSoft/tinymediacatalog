using Domain.Models.Multimedia;

namespace Services.Media.Strategy.Processors;

public interface IFileProcessor <out T> where T : Enum
{
    T MediaKind { get; }
    
    Task<MultimediaFile> PrepareAsync(string path);
}
using Domain.Models.Multimedia;

namespace Services.Media.Strategy;

public interface IFileStrategy
{
    Task<MultimediaFile> PrepareAsync(string path);
}
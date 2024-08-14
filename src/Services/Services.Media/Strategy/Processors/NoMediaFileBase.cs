using Domain.Models.Multimedia;

namespace Services.Media.Strategy.Processors;

public abstract class NoMediaFileBase<T> : IFileProcessor<FileKind> where T : MultimediaFile, new()
{
    public abstract FileKind MediaKind { get; }

    public abstract Task<MultimediaFile> PrepareAsync(string path);

    protected T GetBasicData(string path)
    {
        var result = new T();
        var fileInfo = new FileInfo(path);

        result.FilePath = path;
        result.FileName = Path.GetFileName(path);
        result.Size = fileInfo.Length;

        return result;
    }
}
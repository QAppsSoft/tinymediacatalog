using Domain.Models.Multimedia;

namespace Services.Media.Strategy.Processors;

public class SubtitleProcessor : IFileProcessor<FileKind>
{
    public FileKind MediaKind { get; } = FileKind.Subtitle;
    
    public Task<MultimediaFile> PrepareAsync(string path)
    {
        var result = new OtherFile();
        var fileInfo = new FileInfo(path);

        result.FilePath = path;
        result.FileName = Path.GetFileName(path);
        result.Size = fileInfo.Length;

        return Task.FromResult<MultimediaFile>(result);
    }
}
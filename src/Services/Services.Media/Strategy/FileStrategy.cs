using Domain.Models.Multimedia;
using Services.Media.Strategy.Processors;

namespace Services.Media.Strategy;

public sealed class FileStrategy(IEnumerable<IFileProcessor<FileKind>> fileProcessors, FileKindSelector fileKindSelector) : IFileStrategy
{
    private readonly IEnumerable<IFileProcessor<FileKind>> _fileProcessors = fileProcessors ?? throw new ArgumentNullException(nameof(fileProcessors));
    private readonly FileKindSelector _fileKindSelector = fileKindSelector ?? throw new ArgumentNullException(nameof(fileKindSelector));

    public async Task<MultimediaFile> PrepareAsync(string path)
    {
        var fileKind = await _fileKindSelector.SelectAsync(path).ConfigureAwait(false);

        var processor = _fileProcessors.First(x => x.MediaKind == fileKind);

        return await processor.PrepareAsync(path).ConfigureAwait(false);
    }
}
using Domain.Models.Multimedia;

namespace Services.Media.Strategy.Processors;

public sealed class NfoProcessor : NoMediaFileBase<NfoFile>
{
    public override FileKind MediaKind { get; } = FileKind.Subtitle;
    
    public override Task<MultimediaFile> PrepareAsync(string path)
    {
        return Task.FromResult<MultimediaFile>(GetBasicData(path));
    }
}
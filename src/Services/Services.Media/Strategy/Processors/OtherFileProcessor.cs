using Domain.Models.Multimedia;

namespace Services.Media.Strategy.Processors;

public sealed class OtherFileProcessor : NoMediaFileBase<OtherFile>
{
    public override FileKind MediaKind { get; } = FileKind.Other;
    public override Task<MultimediaFile> PrepareAsync(string path)
    {
        return Task.FromResult<MultimediaFile>(GetBasicData(path));
    }
}
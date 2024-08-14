namespace Services.Media.Strategy.Processors;

public sealed class SubtitleProcessor : NoMediaFileBase
{
    public override FileKind MediaKind { get; } = FileKind.Subtitle;
}
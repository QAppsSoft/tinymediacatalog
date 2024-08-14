namespace Services.Media.Strategy.Processors;

public sealed class OtherFileProcessor : NoMediaFileBase
{
    public override FileKind MediaKind { get; } = FileKind.Other;
}
namespace Domain.Models.Multimedia;

public sealed class ImageFile : MultimediaFile
{
    public override Kind Kind => Kind.Image;
    public Resolution Resolution { get; set; } = Resolution.Zero();
    public string Codec { get; set; } = string.Empty;
}
namespace Domain.Models.Multimedia;

public class ImageFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Image;
    public Resolution Resolution { get; set; }
    public string Codec { get; set; }
}
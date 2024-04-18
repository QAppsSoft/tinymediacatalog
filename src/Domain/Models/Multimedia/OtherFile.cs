namespace Domain.Models.Multimedia;

public sealed class OtherFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Other;
}
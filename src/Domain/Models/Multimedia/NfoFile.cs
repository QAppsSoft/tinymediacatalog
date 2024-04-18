namespace Domain.Models.Multimedia;

public sealed class NfoFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Nfo;
}
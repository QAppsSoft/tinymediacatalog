namespace Domain.Models.Multimedia;

public class NfoFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Nfo;
}
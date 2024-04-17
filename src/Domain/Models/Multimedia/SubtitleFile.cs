namespace Domain.Models.Multimedia;

public class SubtitleFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Subtitle;
    public string Codec { get; set; }
    public string Language { get; set; }
}
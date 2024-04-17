namespace Domain.Models.Multimedia;

public class SubtitleFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Subtitle;
    public string Codec { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}
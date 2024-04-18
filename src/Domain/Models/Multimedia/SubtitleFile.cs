namespace Domain.Models.Multimedia;

public sealed class SubtitleFile : MultimediaFile
{
    public override Kind Kind => Kind.Subtitle;
    public string Codec { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}
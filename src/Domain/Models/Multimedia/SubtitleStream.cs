namespace Domain.Models.Multimedia;

public class SubtitleStream : Stream
{
    public bool Forced { get; set; }
    public bool HearImpaired { get; set; }
    public string Format { get; set; } = string.Empty;
}
namespace Domain.Models.Multimedia;

public class AudioStream : Stream
{
    public string Channels { get; set; } = string.Empty;
    public int BitRate { get; set; }
}
namespace Domain.Models.Multimedia;

public class AudioStream : Stream
{
    public string Channels { get; set; }
    public int BitRate { get; set; }
}
namespace Domain.Models.Multimedia;

public class VideoFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Video;
    public TimeSpan Duration { get; set; }
    public string Codec { get; set; }
    public Resolution Resolution { get; set; }
    public string FrameRate { get; set; }
    public int BitRate { get; set; }
    
    public IList<AudioStream> Audios { get; set; }
    public IList<SubtitleStream> Subtitles { get; set; }
}
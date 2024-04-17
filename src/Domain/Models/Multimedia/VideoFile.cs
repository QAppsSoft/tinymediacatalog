namespace Domain.Models.Multimedia;

public class VideoFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Video;
    public TimeSpan Duration { get; set; }
    public string Codec { get; set; } = string.Empty;
    public Resolution Resolution { get; set; } = Resolution.Zero();
    public string FrameRate { get; set; } = string.Empty;
    public int BitRate { get; set; }

    public IList<AudioStream> Audios { get; set; } = new List<AudioStream>();
    public IList<SubtitleStream> Subtitles { get; set; } = new List<SubtitleStream>();
}
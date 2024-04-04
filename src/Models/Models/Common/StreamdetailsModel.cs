namespace Models.Common;

public class StreamdetailsModel
{
    public VideoModel Video { get; set; }

    public List<AudioModel> Audio { get; set; }

    public SubtitleModel Subtitle { get; set; }
}
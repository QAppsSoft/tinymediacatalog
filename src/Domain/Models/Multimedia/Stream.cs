namespace Domain.Models.Multimedia;

public abstract class Stream
{
    public string Title { get; set; }
    public Source Source { get; set; }
    public string Language { get; set; }
}
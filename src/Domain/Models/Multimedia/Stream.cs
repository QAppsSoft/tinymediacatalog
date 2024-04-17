namespace Domain.Models.Multimedia;

public abstract class Stream
{
    public string Title { get; set; } = string.Empty;
    public Source Source { get; set; }
    public string Language { get; set; } = string.Empty;
}
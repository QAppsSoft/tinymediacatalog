namespace Domain.Models.Movie;

public sealed class ActorMovie
{
    public string Role { get; set; } = string.Empty;
    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public Guid MovieContainerId { get; set; }
    public MovieContainer Movie { get; set; } = null!;
}
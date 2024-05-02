namespace Domain.Models.Movie;

public sealed class Actor
{
    public string Role { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public Guid MovieContainerId { get; set; }
    public MovieContainer Movie { get; set; } = null!;
}
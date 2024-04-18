using Domain.Models.Movie;

namespace Domain.Models;

public sealed class ActorMovie
{
    public string Role { get; set; } = string.Empty;
    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public Guid MovieContainerId { get; set; }
    public MovieContainer Movie { get; set; } = null!;
}
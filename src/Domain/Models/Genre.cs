using Domain.Models.BaseObjects;
using Domain.Models.Movie;

namespace Domain.Models;

public sealed class Genre : GuidEntityBase
{
    public string Name { get; set; } = string.Empty;
    
    public ICollection<MovieContainer> Movies { get; set; } = new List<MovieContainer>();
}
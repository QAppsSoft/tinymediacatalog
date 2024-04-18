using Domain.Models.BaseObjects;

namespace Domain.Models;

public sealed class Genre : GuidEntityBase
{
    public string Name { get; set; } = string.Empty;
}
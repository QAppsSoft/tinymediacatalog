using Domain.Models.BaseObjects;

namespace Domain.Models;

public abstract class Genre : GuidEntityBase
{
    public string Name { get; set; }
}
using Domain.Models.BaseObjects;

namespace Domain.Models;

public abstract class Actor : GuidEntityBase
{
    public string Role { get; set; } = string.Empty;
    public Person Person { get; set; } = null!; // Should be initialized on load
}
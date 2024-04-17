using Domain.Models.BaseObjects;

namespace Domain.Models;

public abstract class Actor : GuidEntityBase
{
    public string Role { get; set; }
    public Person Person { get; set; }
}
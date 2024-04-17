using Domain.Models.BaseObjects;

namespace Domain.Models;

public abstract class Person : GuidEntityBase
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Thumb { get; set; }
    public string Profile { get; set; }
    public IList<ServiceId> ServicesId { get; set; }
}
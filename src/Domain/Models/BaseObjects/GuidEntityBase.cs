namespace Domain.Models.BaseObjects;

public class GuidEntityBase : EntityBase<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
}
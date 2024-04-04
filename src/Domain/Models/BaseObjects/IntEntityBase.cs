namespace Domain.Models.BaseObjects;

public class IntEntityBase : EntityBase<int>
{
    public override int Id { get; set; }
}
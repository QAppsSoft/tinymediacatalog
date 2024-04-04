namespace Domain.Models.BaseObjects;

public abstract class EntityBase<T>
{
    public abstract T Id { get; set; }
}
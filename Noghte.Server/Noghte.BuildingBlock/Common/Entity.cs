namespace Noghte.BuildingBlock.Common;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
}

public abstract class Entity : Entity<long>
{
}
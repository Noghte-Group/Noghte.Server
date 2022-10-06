namespace Noghte.BuildingBlock.Common;

public interface IEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
}

public interface IEntity
{
    
}
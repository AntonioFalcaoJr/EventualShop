namespace Domain.Abstractions.Entities
{
    public abstract class Entity<TId> : IEntity
        where TId : struct
    {
        public TId Id { get; protected init; }
    }
}
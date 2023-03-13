namespace Domain.Abstractions.Entities;

public interface IEntity<out TId> 
    where TId : IIdentifier
{
    TId Id { get; }
    bool IsDeleted { get; }
}
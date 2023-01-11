namespace Domain.Abstractions.Entities;

public interface IEntity
{
    Guid Id { get; }
    bool IsDeleted { get; }
}
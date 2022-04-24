namespace ECommerce.Abstractions;

public interface IProjection
{
    Guid Id { get; }
    bool IsDeleted { get; }
}
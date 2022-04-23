namespace ECommerce.Abstractions.Projections;

public interface IProjection
{
    Guid Id { get; }
    bool IsDeleted { get; }
}
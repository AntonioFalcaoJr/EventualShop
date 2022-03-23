namespace Application.Abstractions.EventSourcing.Projections;

public interface IProjection
{
    Guid Id { get; }
    bool IsDeleted { get; }
}
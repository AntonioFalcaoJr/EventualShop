using MassTransit.Topology;

namespace Application.Abstractions.EventSourcing.Projections;

[ExcludeFromTopology]
public interface IProjection
{
    Guid Id { get; }
    bool IsDeleted { get; }
}
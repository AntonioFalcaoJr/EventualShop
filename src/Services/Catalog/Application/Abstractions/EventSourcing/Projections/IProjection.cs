using System;
using MassTransit.Topology;

namespace Application.Abstractions.EventSourcing.Projections
{
    [ExcludeFromTopology]
    public interface IProjection
    {
        Guid AggregateId { get; }
        bool IsDeleted { get; }
    }
}
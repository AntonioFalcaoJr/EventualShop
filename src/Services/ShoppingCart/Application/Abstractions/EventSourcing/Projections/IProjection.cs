using System;
using MassTransit.Topology;

namespace Application.Abstractions.EventSourcing.Projections
{
    [ExcludeFromTopology]
    public interface IProjection
    {
        public Guid Id { get; }
    }
}
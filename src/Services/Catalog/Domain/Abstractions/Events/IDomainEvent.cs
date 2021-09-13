using System;
using MassTransit.Topology;

namespace Domain.Abstractions.Events
{
    [ExcludeFromTopology]
    public interface IDomainEvent
    {
        DateTimeOffset Timestamp { get; }
    }
}
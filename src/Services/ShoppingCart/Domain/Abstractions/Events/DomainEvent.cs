using System;
using MassTransit.Topology;

namespace Domain.Abstractions.Events
{
    [ExcludeFromTopology]
    public abstract record DomainEvent : IDomainEvent
    {
        public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
        public int AggregateVersion { get; set; }
    }
}
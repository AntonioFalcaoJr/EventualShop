using System;
using MassTransit.Topology;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public abstract record Message(Guid CorrelationId = default) : IMessage
{
    public DateTimeOffset Timestamp { get; private init; } = DateTimeOffset.Now;
    public Guid CorrelationId { get; } = CorrelationId == Guid.Empty ? Guid.NewGuid() : CorrelationId;
}
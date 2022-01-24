using System;
using MassTransit;
using MassTransit.Topology;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public interface IMessage : CorrelatedBy<Guid>
{
    DateTimeOffset Timestamp { get; }
}
using System;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Events;

[ExcludeFromTopology]
public abstract record Event(Guid CorrelationId = default) : Message(CorrelationId), IEvent;
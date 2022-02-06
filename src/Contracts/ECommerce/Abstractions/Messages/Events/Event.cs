using System;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Events;

[ExcludeFromTopology]
public abstract record Event(Guid CorrelationId = default) : Message(CorrelationId), IEvent;
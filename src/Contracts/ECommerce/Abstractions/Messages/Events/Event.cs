using System;
using MassTransit;

namespace ECommerce.Abstractions.Messages.Events;

[ExcludeFromTopology]
public abstract record Event(Guid CorrelationId = default) : Message(CorrelationId), IEvent;
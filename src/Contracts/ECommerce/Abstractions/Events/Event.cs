using MassTransit.Topology;

namespace ECommerce.Abstractions.Events;

[ExcludeFromTopology]
public abstract record Event : Message, IEvent;
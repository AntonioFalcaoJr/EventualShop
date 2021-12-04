using MassTransit.Topology;

namespace ECommerce.Abstractions.Events;

[ExcludeFromTopology]
public interface IEvent : IMessage { }
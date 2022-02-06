using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Events;

[ExcludeFromTopology]
public interface IEvent : IMessage { }
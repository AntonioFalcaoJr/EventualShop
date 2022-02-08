using MassTransit;

namespace ECommerce.Abstractions.Messages.Events;

[ExcludeFromTopology]
public interface IEvent : IMessage { }
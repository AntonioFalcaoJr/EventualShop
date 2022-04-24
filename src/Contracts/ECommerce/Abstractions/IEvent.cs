using MassTransit;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public interface IEvent : IMessage { }
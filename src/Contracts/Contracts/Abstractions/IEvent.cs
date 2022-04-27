using MassTransit;

namespace Contracts.Abstractions;

[ExcludeFromTopology]
public interface IEvent : IMessage { }
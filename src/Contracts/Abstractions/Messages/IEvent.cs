using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IEvent : IMessage { }

public interface IVersionedEvent : IEvent
{
    long Version { get; }
}
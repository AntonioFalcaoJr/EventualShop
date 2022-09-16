using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IEvent : IMessage
{
    string Name => GetType().Name;
}

// TODO - Remove it!
[ExcludeFromTopology]
public interface IEventWithId : IEvent
{
    Guid Id { get; }
}
using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IEvent : IMessage { }

// TODO - Remove it!
[ExcludeFromTopology]
public interface IEventWithId : IMessage
{
    Guid Id { get; }
}
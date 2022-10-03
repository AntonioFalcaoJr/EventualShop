using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IEvent : IMessage
{
    Guid Id { get; }
}
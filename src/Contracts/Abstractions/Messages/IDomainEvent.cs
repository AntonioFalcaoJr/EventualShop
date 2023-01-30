using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IDomainEvent : IEvent
{
    long Version { get; }
}
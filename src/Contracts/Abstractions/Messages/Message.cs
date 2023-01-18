using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public abstract record Message : IMessage
{
    public DateTimeOffset Timestamp { get; private init; } = DateTimeOffset.Now;
}
using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public abstract record Message
{
    public DateTimeOffset Timestamp { get; private init; } = DateTimeOffset.Now;
}
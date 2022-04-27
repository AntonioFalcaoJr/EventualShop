using MassTransit;

namespace Contracts.Abstractions;

[ExcludeFromTopology]
public abstract record Message(Guid CorrelationId = default) : IMessage
{
    // TODO - Verify deserialization without private init;
    public DateTimeOffset Timestamp { get; private init; } = DateTimeOffset.Now;
    public Guid CorrelationId { get; } = CorrelationId == Guid.Empty ? Guid.NewGuid() : CorrelationId;
}
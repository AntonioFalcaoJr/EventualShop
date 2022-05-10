using MassTransit;

namespace Contracts.Abstractions;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
    Guid? CorrelationId { get; }
}
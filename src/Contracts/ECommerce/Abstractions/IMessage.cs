using MassTransit;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
    Guid CorrelationId { get; }
}
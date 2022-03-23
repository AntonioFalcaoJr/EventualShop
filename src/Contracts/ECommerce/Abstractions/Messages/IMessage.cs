using MassTransit;

namespace ECommerce.Abstractions.Messages;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
    Guid CorrelationId { get; }
}
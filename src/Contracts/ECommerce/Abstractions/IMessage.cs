using MassTransit.Topology;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
}
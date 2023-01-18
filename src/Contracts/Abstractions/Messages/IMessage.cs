using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
}
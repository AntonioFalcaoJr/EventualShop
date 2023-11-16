using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public abstract record Event : Message, IEvent;
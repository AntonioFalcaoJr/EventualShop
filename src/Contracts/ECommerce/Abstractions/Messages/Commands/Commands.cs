using System;
using MassTransit;

namespace ECommerce.Abstractions.Messages.Commands;

[ExcludeFromTopology]
public abstract record Command(Guid CorrelationId = default) : Message(CorrelationId), ICommand;
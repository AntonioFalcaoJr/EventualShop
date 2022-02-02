using System;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Commands;

[ExcludeFromTopology]
public abstract record Command(Guid CorrelationId = default) : Message(CorrelationId), ICommand;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Commands;

[ExcludeFromTopology]
public abstract record Command : Message, ICommand;
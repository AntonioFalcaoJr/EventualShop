using MassTransit.Topology;

namespace ECommerce.Abstractions.Commands;

[ExcludeFromTopology]
public record Command : Message, ICommand;
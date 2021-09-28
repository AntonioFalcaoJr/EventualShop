using MassTransit.Topology;

namespace Messages.Abstractions.Commands
{
    [ExcludeFromTopology]
    public record Command : Message, ICommand;
}
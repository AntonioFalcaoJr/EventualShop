using MassTransit.Topology;

namespace Messages.Abstractions.Commands
{
    [ExcludeFromTopology]
    public interface ICommand : IMessage { }
}
using MassTransit.Topology;

namespace Messages.Abstractions.Queries
{
    [ExcludeFromTopology]
    public interface IQuery : IMessage { }
}
using MassTransit.Topology;

namespace Messages.Abstractions
{
    [ExcludeFromTopology]
    public interface IQuery { }
}
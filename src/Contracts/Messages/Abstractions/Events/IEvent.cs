using MassTransit.Topology;

namespace Messages.Abstractions.Events
{
    [ExcludeFromTopology]
    public interface IEvent : IMessage { }
}
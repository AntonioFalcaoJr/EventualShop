using MassTransit.Topology;

namespace Messages.Abstractions.Events
{
    [ExcludeFromTopology]
    public abstract record Event : Message, IEvent;
}
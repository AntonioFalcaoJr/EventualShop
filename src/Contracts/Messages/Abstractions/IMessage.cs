using System;
using MassTransit.Topology;

namespace Messages.Abstractions
{
    [ExcludeFromTopology]
    public interface IMessage<out TId>
        where TId : struct
    {
        TId Id { get; }
        DateTimeOffset Timestamp { get; }
    }
}
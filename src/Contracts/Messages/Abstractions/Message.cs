using System;
using MassTransit.Topology;

namespace Messages.Abstractions
{
    [ExcludeFromTopology]
    public abstract record Message<TId>(TId Id) : IMessage<TId>
        where TId : struct
    {
        public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
    }
}
using System;
using MassTransit.Topology;

namespace Messages.Abstractions
{
    [ExcludeFromTopology]
    public abstract record Message : IMessage
    {
        public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
    }
}
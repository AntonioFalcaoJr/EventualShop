using System;
using MassTransit.Topology;

namespace Messages.Abstractions;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
}
using MassTransit.Topology;

namespace Messages.Abstractions.Queries;

[ExcludeFromTopology]
public abstract record Query : Message, IQuery;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Queries;

[ExcludeFromTopology]
public abstract record Query : Message, IQuery;
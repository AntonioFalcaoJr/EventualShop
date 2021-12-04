using MassTransit.Topology;

namespace ECommerce.Abstractions.Queries.Responses;

[ExcludeFromTopology]
public abstract record Response : Message, IResponse;
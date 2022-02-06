using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Queries.Responses;

[ExcludeFromTopology]
public abstract record Response : Message, IResponse;
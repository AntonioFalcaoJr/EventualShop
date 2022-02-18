using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries.Responses;

[ExcludeFromTopology]
public abstract record Response : Message, IResponse;

public record NotFound : Response;
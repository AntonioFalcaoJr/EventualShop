using ECommerce.Abstractions;

namespace ECommerce.Contracts.Identities;

public static class Query
{
    public record GetUserAuthentication(Guid UserId) : Message(CorrelationId: UserId), IQuery;
}
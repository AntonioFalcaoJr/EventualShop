using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Identities;

public static class Query
{
    public record GetUserAuthentication(Guid UserId) : Message(CorrelationId: UserId), IQuery;
}
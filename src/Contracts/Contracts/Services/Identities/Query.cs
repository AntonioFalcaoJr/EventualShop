using Contracts.Abstractions;

namespace Contracts.Services.Identities;

public static class Query
{
    public record GetUserAuthentication(Guid UserId) : Message(CorrelationId: UserId), IQuery;
}
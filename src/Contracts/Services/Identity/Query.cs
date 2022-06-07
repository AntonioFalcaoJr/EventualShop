using Contracts.Abstractions;

namespace Contracts.Services.Identity;

public static class Query
{
    public record GetUserAuthentication(Guid UserId) : Message(CorrelationId: UserId), IQuery;
}
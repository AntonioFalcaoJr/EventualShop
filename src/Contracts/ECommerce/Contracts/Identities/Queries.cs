using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Identities;

public static class Queries
{
    public record GetUserAuthenticationDetails(Guid UserId) : Query;
}
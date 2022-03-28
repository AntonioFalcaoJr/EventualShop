using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Identity;

public static class Queries
{
    public record GetUserAuthenticationDetails(Guid UserId) : Query;
}
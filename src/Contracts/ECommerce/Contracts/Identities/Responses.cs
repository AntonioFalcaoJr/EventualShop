using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Identities;

public static class Responses
{
    public record UserAuthenticationDetails(Guid UserId, string Password, string UserName) : Response;
}
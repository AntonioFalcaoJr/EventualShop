using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Accounts;

public static class Responses
{
    public record AccountDetails(Guid Id, string Password, string UserName) : Response;

    public record AccountsDetailsPagedResult : ResponsePagedResult<AccountDetails>;
}
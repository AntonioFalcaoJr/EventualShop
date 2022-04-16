using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Accounts;

public static class Responses
{
    public record Account(Guid Id, string Password, string UserName) : Response;

    public record Accounts  : ResponsePaged<Account>;
}
using System;
using ECommerce.Abstractions.Queries.Responses;

namespace ECommerce.Contracts.Account;

public static class Responses
{
    public record NotFound(string Message = "Not found.") : Response;

    public record AccountDetails(Guid Id, string Password, string UserName) : Response;

    public record AccountsDetailsPagedResult : ResponsePagedResult<AccountDetails>;
}
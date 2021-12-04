using System;
using ECommerce.Abstractions.Queries.Responses;

namespace ECommerce.Contracts.Account;

public static class Responses
{
    public record AccountDetails(Guid AccountId, string Password, string UserName) : Response;

    public record AccountsDetailsPagedResult : ResponsePagedResult<AccountDetails>;
}
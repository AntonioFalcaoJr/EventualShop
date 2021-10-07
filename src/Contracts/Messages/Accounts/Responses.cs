using System;
using Messages.Abstractions.Queries.Responses;

namespace Messages.Accounts
{
    public static class Responses
    {
        public record AccountDetails(Guid AccountId, string Password, string UserName) : Response;

        public record AccountsDetailsPagedResult : ResponsePagedResult<AccountDetails>;
    }
}
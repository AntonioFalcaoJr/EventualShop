using System;
using System.Collections.Generic;
using Messages.Abstractions.Queries.Paging;
using Messages.Abstractions.Queries.Responses;

namespace Messages.Accounts
{
    public static class Responses
    {
        public record AccountDetails(Guid AccountId, string Password, string UserName) : Response;

        public record AccountsDetailsPagedResult(IEnumerable<AccountDetails> Items, IPageInfo PageInfo)
            : ResponsePagedResult<AccountDetails>(Items, PageInfo);
    }
}
using System;
using System.Collections.Generic;
using Messages.Abstractions.Queries.Paging;
using Messages.Abstractions.Queries.Responses;
using Response = Messages.Abstractions.Queries.Responses.Response;

namespace Messages.Accounts
{
    public static class Responses
    {
        public record AccountDetails(Guid Id, string Password, string UserName) : Response;

        public record AccountsDetailsPagedResult(IEnumerable<AccountDetails> Items, IPageInfo PageInfo) 
            : ResponsePagedResult<AccountDetails>(Items, PageInfo);
    }
}
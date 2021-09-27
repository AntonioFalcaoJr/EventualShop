using System;
using System.Collections.Generic;
using Messages.Abstractions.Paging;

namespace Messages.Accounts
{
    public static class Responses
    {
        public record AccountDetails(Guid Id, string Password, string UserName);

        public record AccountsDetailsPagedResult(IEnumerable<AccountDetails> Items, IPageInfo PageInfo) : IPagedResult<AccountDetails>;
    }
}
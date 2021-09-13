using System;
using Messages.Accounts.Queries;

namespace ECommerce.WebAPI.Messages.Accounts
{
    public static class Queries
    {
        public record GetAccountDetailsQuery(Guid Id) : GetAccountDetails;

        public record GetAccountsDetailsWithPaginationQuery(int Limit, int Offset) : GetAccountsDetailsWithPagination;
    }
}
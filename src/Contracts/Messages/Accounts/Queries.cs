using System;
using Messages.Abstractions.Paging;

namespace Messages.Accounts
{
    public static class Queries
    {
        public record GetAccountDetails(Guid Id);

        public record GetAccountsDetailsWithPagination(int Limit, int Offset) : IPaging;
    }
}
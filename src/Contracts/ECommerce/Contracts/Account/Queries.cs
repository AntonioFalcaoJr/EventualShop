using System;
using ECommerce.Abstractions.Messages.Queries;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace ECommerce.Contracts.Account;

public static class Queries
{
    public record GetAccountDetails(Guid AccountId) : Query;

    public record GetAccounts(IPaging Paging) : QueryPaging(Paging);
}
using ECommerce.Abstractions.Queries;

namespace ECommerce.Contracts.Account;

public static class Queries
{
    public record GetAccountDetails(Guid AccountId) : Query;

    public record GetAccountsDetailsWithPagination(int Limit, int Offset) : QueryPaging(Limit, Offset);
}
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Accounts;

public static class Queries
{
    public record GetAccountDetails(Guid AccountId) : Query;

    public record GetAccounts(int Limit, int Offset) : QueryPaging(Limit, Offset);
}
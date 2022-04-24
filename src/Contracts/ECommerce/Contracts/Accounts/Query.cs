using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Accounts;

public static class Query
{
    public record GetAccount(Guid AccountId) : Message(CorrelationId: AccountId), IQuery;

    public record GetAccounts(int Limit, int Offset) : Message, IQuery;
}
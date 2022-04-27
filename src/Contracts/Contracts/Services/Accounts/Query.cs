using Contracts.Abstractions;

namespace Contracts.Services.Accounts;

public static class Query
{
    public record GetAccount(Guid AccountId) : Message(CorrelationId: AccountId), IQuery;

    public record GetAccounts(int Limit, int Offset) : Message, IQuery;
}
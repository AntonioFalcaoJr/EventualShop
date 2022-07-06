using Contracts.Abstractions.Messages;

namespace Contracts.Services.Account;

public static class Query
{
    public record GetAccount(Guid AccountId) : Message(CorrelationId: AccountId), IQuery;

    public record ListAccounts(int Limit, int Offset) : Message, IQuery;
}
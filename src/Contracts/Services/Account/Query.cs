using Contracts.Abstractions.Messages;

namespace Contracts.Services.Account;

public static class Query
{
    public record GetAccount(Guid AccountId) : Message, IQuery;

    public record ListAccounts(ushort Limit, ushort Offset) : Message, IQuery;

    public record ListAddresses(Guid AccountId, ushort Limit, ushort Offset) : Message, IQuery;
}
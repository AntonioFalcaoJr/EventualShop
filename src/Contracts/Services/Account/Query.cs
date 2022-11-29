using Contracts.Abstractions.Messages;

namespace Contracts.Services.Account;

public static class Query
{
    public record GetAccount(Guid AccountId) : IQuery;

    public record ListAccounts(ushort Limit, ushort Offset) : IQuery;

    public record ListShippingAddresses(Guid AccountId, ushort Limit, ushort Offset) : IQuery;
}
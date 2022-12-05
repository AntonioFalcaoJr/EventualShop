using Contracts.Abstractions.Messages;
using Contracts.Services.Account.Protobuf;

namespace Contracts.Services.Account;

public static class Query
{
    public record GetAccount(Guid AccountId) : IQuery
    {
        public static implicit operator GetAccount(GetAccountRequest request)
            => new(new Guid(request.Id));
    }

    public record ListAccounts(ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListAccounts(ListAccountsRequest request)
            => new((ushort)request.Limit, (ushort)request.Offset);
    }

    public record ListShippingAddresses(Guid AccountId, ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListShippingAddresses(ListShippingAddressesRequest request)
            => new(new(request.AccountId), (ushort)request.Limit, (ushort)request.Offset);
    }
}
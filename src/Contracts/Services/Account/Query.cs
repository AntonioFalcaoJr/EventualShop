using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account.Protobuf;

namespace Contracts.Services.Account;

public static class Query
{
    public record struct GetAccount(Guid AccountId) : IQuery
    {
        public static implicit operator GetAccount(GetAccountRequest request)
            => new(new(request.AccountId));
    }

    public record struct ListAccounts(Paging Paging) : IQuery
    {
        public static implicit operator ListAccounts(ListAccountsRequest request)
            => new(request.Paging);
    }

    public record struct ListShippingAddresses(Guid AccountId, Paging Paging) : IQuery
    {
        public static implicit operator ListShippingAddresses(ListShippingAddressesRequest request)
            => new(new(request.AccountId), request.Paging);
    }
}
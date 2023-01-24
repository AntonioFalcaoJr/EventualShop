using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account.Protobuf;

namespace Contracts.Services.Account;

public static class Query
{
    public record GetAccountDetails(Guid AccountId) : IQuery
    {
        public static implicit operator GetAccountDetails(GetAccountDetailsRequest request)
            => new(new Guid(request.AccountId));
    }

    public record ListAccountsDetails(Paging Paging) : IQuery
    {
        public static implicit operator ListAccountsDetails(ListAccountsDetailsRequest request)
            => new(request.Paging);
    }

    public record ListShippingAddressesListItems(Guid AccountId, Paging Paging) : IQuery
    {
        public static implicit operator ListShippingAddressesListItems(ListShippingAddressesListItemsRequest request)
            => new(new(request.AccountId), request.Paging);
    }
}
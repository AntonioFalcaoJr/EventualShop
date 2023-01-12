using Contracts.Services.Account.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Accounts.Validators;

namespace WebAPI.APIs.Accounts;

public static class Queries
{
   public record GetAccountDetails(AccountService.AccountServiceClient Client, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<GetAccountValidator>, IQuery<AccountService.AccountServiceClient>
    {
        public static implicit operator GetAccountDetailsRequest(GetAccountDetails request)
            => new() { AccountId = request.AccountId.ToString() };
    }

    public record ListAccountsDetails(AccountService.AccountServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListAccountsDetailsValidator>, IQuery<AccountService.AccountServiceClient>
    {
        public static implicit operator ListAccountsDetailsRequest(ListAccountsDetails request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListShippingAddressesListItems(AccountService.AccountServiceClient Client, Guid AccountId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListShippingAddressesListItemsValidator>, IQuery<AccountService.AccountServiceClient>
    {
        public static implicit operator ListShippingAddressesListItemsRequest(ListShippingAddressesListItems request)
            => new() { AccountId = request.AccountId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}
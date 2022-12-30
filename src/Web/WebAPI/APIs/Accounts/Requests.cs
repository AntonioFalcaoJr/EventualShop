using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using Contracts.Services.Account.Protobuf;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Accounts.Validators;

namespace WebAPI.APIs.Accounts;

public static class Requests
{
    public record AddShippingAddress(IBus Bus, Guid AccountId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddShippingAddressValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddShippingAddress(AccountId, Address);
    }

    public record AddBillingAddress(IBus Bus, Guid AccountId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddBillingAddressValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddBillingAddress(AccountId, Address);
    }

    public record ListShippingAddressesListItems(AccountService.AccountServiceClient Client, Guid AccountId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListShippingAddressesListItemsValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator ListShippingAddressesListItemsRequest(ListShippingAddressesListItems request)
            => new() { AccountId = request.AccountId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record DeleteAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<DeleteAccountValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.DeleteAccount(AccountId);
    }

    public record GetAccountDetails(AccountService.AccountServiceClient Client, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<GetAccountValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator GetAccountDetailsRequest(GetAccountDetails request)
            => new() { AccountId = request.AccountId.ToString() };
    }

    public record ListAccountsDetails(AccountService.AccountServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListAccountsDetailsValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator ListAccountsDetailsRequest(ListAccountsDetails request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}
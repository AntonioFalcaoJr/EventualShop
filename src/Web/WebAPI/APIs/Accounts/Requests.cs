using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using Contracts.Services.Account.Grpc;
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

    public record ListShippingAddresses(AccountService.AccountServiceClient Client, Guid AccountId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListShippingAddressesValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator ListShippingAddressesRequest(ListShippingAddresses request)
            => new()
            {
                AccountId = request.AccountId.ToString(),
                Limit = request.Limit ?? default,
                Offset = request.Offset ?? default
            };
    }

    public record DeleteAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<DeleteAccountValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.DeleteAccount(AccountId);
    }

    public record GetAccount(AccountService.AccountServiceClient Client, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<GetAccountValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator GetAccountRequest(GetAccount request)
            => new() { Id = request.AccountId.ToString() };
    }

    public record ListAccounts(AccountService.AccountServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListAccountsValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator ListAccountsRequest(ListAccounts request)
            => new() { Limit = request.Limit ?? default, Offset = request.Offset ?? default };
    }
}
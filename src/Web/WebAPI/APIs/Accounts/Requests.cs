using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using Contracts.Services.Account.Grpc;
using Contracts.Services.Identity.Grpc;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Accounts.Validators;
using WebAPI.APIs.Identities.Validators;

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

    public record struct ListAddresses(IBus Bus, Guid AccountId, ushort? Limit, ushort? Offset, CancellationToken CancellationToken)
    {
        public static implicit operator Query.ListAddresses(ListAddresses request)
            => new(request.AccountId, request.Limit ?? 0, request.Offset ?? 0);
    }

    public record DeleteAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<DeleteAccountValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.DeleteAccount(AccountId);
    }

    public record GetAccount(AccountService.AccountServiceClient Client, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<SignInValidator>, IQueryRequest<AccountService.AccountServiceClient>
    {
        public static implicit operator GetAccountRequest(GetAccount request)
            => new() { Id = request.AccountId.ToString() };
    }

    public record struct ListAccounts(IBus Bus, ushort? Limit, ushort? Offset, CancellationToken CancellationToken)
    {
        public static implicit operator Query.ListAccounts(ListAccounts request)
            => new(request.Limit ?? 0, request.Offset ?? 0);
    }
}
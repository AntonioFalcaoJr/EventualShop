using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Accounts.Validators;

namespace WebAPI.APIs.Accounts;

public static class Requests
{
    public record AddShippingAddress(IBus Bus, Guid AccountId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddShippingAddressValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.AddShippingAddress(AccountId, Address);
    }

    public record AddBillingAddress(IBus Bus, Guid AccountId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddBillingAddressValidator>, ICommandRequest
    {
        public ICommand AsCommand()
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
        public ICommand AsCommand()
            => new Command.DeleteAccount(AccountId);
    }

    public record struct GetAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
    {
        public static implicit operator Query.GetAccount(GetAccount request)
            => new(request.AccountId);
    }

    public record CreateAccount(IBus Bus, Payloads.CreateAccount Payload, CancellationToken CancellationToken)
        : Validatable<CreateAccountValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.CreateAccount(Guid.NewGuid(), Payload.Email, Payload.Password, Payload.PasswordConfirmation, 
                Payload.AcceptedPolicies, Payload.WishToReceiveNews);
    }

    public record struct ListAccounts(IBus Bus, ushort? Limit, ushort? Offset, CancellationToken CancellationToken)
    {
        public static implicit operator Query.ListAccounts(ListAccounts request)
            => new(request.Limit ?? 0, request.Offset ?? 0);
    }
}
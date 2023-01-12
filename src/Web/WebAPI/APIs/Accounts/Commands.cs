using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Accounts.Validators;

namespace WebAPI.APIs.Accounts;

public static class Commands
{
    public record AddShippingAddress(IBus Bus, Guid AccountId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddShippingAddressValidator>, ICommand<Command.AddShippingAddress>
    {
        public Command.AddShippingAddress Command => new(AccountId, Address);
    }

    public record AddBillingAddress(IBus Bus, Guid AccountId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddBillingAddressValidator>, ICommand<Command.AddBillingAddress>
    {
        public Command.AddBillingAddress Command => new(AccountId, Address);
    }

    public record DeleteAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
        : Validatable<DeleteAccountValidator>, ICommand<Command.DeleteAccount>
    {
        public Command.DeleteAccount Command => new(AccountId);
    }
}
using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Command
{
    public record CreateAccount(Dto.Profile Profile, string Password, string PasswordConfirmation, bool AcceptedPolicies, bool WishToReceiveNews) : Message, ICommand;

    public record AddShippingAddress(Guid AccountId, Dto.Address Address) : Message, ICommand;

    public record AddBillingAddress(Guid AccountId, Dto.Address Address) : Message, ICommand;

    public record DeleteAccount(Guid AccountId) : Message, ICommand;
}
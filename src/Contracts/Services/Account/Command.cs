using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Command
{
    public record CreateAccount(Guid AccountId, Dto.Profile Profile, string Password, string PasswordConfirmation, bool AcceptedPolicies, bool WishToReceiveNews) : Message(CorrelationId: AccountId), ICommand;

    public record AddShippingAddress(Guid AccountId, Dto.Address Address) : Message(CorrelationId: AccountId), ICommand;

    public record AddBillingAddress(Guid AccountId, Dto.Address Address) : Message(CorrelationId: AccountId), ICommand;

    public record DeleteAccount(Guid AccountId) : Message(CorrelationId: AccountId), ICommand;

    public record PreferShippingAddress(Guid AccountId, Guid AddressId) : Message(CorrelationId: AccountId), ICommand;

    public record PreferBillingAddress(Guid AccountId, Guid AddressId) : Message(CorrelationId: AccountId), ICommand;
}
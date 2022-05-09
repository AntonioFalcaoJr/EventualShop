using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class DomainEvent
{
    public record AccountDeleted(Guid AccountId) : Message, IEvent;

    public record AccountCreated(Guid AccountId, Dto.Profile Profile, string Password, string PasswordConfirmation, bool AcceptedPolicies, bool WishToReceiveNews) : Message, IEvent;

    public record BillingAddressAdded(Guid AccountId, Dto.Address Address) : Message, IEvent;

    public record ShippingAddressAdded(Guid AccountId, Dto.Address Address) : Message, IEvent;
}
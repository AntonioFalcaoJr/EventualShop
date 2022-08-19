using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class DomainEvent
{
    public record AccountDeleted(Guid AccountId) : Message, IEvent;
    
    public record AccountDeactivated(Guid Id) : Message(CorrelationId: Id), IEventWithId;

    public record AccountCreated(Guid AccountId, Dto.Profile Profile, string Password, string PasswordConfirmation, bool AcceptedPolicies, bool WishToReceiveNews) : Message, IEvent;

    public record BillingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address) : Message, IEvent;

    public record ShippingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address) : Message, IEvent;

    public record BillingAddressPreferred(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record ShippingAddressPreferred(Guid AccountId, Guid AddressId) : Message, IEvent;
}
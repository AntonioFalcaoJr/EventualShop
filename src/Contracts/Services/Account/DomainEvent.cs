using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class DomainEvent
{
    public record AccountDeleted(Guid AccountId) : Message, IEvent;

    public record BillingAddressDeleted(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record ShippingAddressDeleted(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record AccountDeactivated(Guid AccountId) : Message, IEvent;

    public record AccountCreated(Guid AccountId, string FirstName, string LastName, string Email) : Message, IEvent;

    public record AccountActivated(Guid AccountId) : Message, IEvent;

    public record BillingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address) : Message, IEvent;

    public record BillingAddressRestored(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record ShippingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address) : Message, IEvent;

    public record ShippingAddressRestored(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record BillingAddressPreferred(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record ShippingAddressPreferred(Guid AccountId, Guid AddressId) : Message, IEvent;
}
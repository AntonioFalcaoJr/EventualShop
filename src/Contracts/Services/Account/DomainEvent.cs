using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class DomainEvent
{
    public record AccountDeleted(Guid AccountId, string Status, long Version) : Message, IDomainEvent;

    public record BillingAddressDeleted(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record ShippingAddressDeleted(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record AccountDeactivated(Guid AccountId, string Status, long Version) : Message, IDomainEvent;

    public record AccountCreated(Guid AccountId, string FirstName, string LastName, string Email, string Status, long Version) : Message, IDomainEvent;

    public record AccountActivated(Guid AccountId, string Status, long Version) : Message, IDomainEvent;

    public record BillingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address, long Version) : Message, IDomainEvent;

    public record BillingAddressRestored(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record ShippingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address, long Version) : Message, IDomainEvent;

    public record ShippingAddressRestored(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record BillingAddressPreferred(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record ShippingAddressPreferred(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record PrimaryBillingAddressRemoved(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;

    public record PrimaryShippingAddressRemoved(Guid AccountId, Guid AddressId, long Version) : Message, IDomainEvent;
}
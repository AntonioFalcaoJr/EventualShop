using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class DomainEvent
{
    public record AccountDeleted(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record BillingAddressDeleted(Guid Id, Guid AddressId) : Message(CorrelationId: Id), IEvent;

    public record ShippingAddressDeleted(Guid Id, Guid AddressId) : Message(CorrelationId: Id), IEvent;

    public record AccountDeactivated(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record AccountCreated(Guid Id, string FirstName, string LastName, string Email) : Message(CorrelationId: Id), IEvent;

    public record AccountActivated(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record BillingAddressAdded(Guid Id, Guid AddressId, Dto.Address Address) : Message(CorrelationId: Id), IEvent;

    public record BillingAddressRestored(Guid Id, Guid AddressId) : Message(CorrelationId: Id), IEvent;

    public record ShippingAddressAdded(Guid Id, Guid AddressId, Dto.Address Address) : Message(CorrelationId: Id), IEvent;

    public record ShippingAddressRestored(Guid Id, Guid AddressId) : Message(CorrelationId: Id), IEvent;

    public record BillingAddressPreferred(Guid Id, Guid AddressId) : Message(CorrelationId: Id), IEvent;

    public record ShippingAddressPreferred(Guid Id, Guid AddressId) : Message(CorrelationId: Id), IEvent;
}
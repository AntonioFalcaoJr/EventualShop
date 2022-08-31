using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class DomainEvent
{
    public record AccountDeleted(Guid Id) : Message(CorrelationId: Id), IEventWithId;

    public record AccountDeactivated(Guid Id) : Message(CorrelationId: Id), IEventWithId;

    public record AccountCreated(Guid Id, string FirstName, string LastName, string Email) : Message(CorrelationId: Id), IEventWithId;

    public record BillingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address) : Message, IEvent;

    public record ShippingAddressAdded(Guid AccountId, Guid AddressId, Dto.Address Address) : Message, IEvent;

    public record BillingAddressPreferred(Guid AccountId, Guid AddressId) : Message, IEvent;

    public record ShippingAddressPreferred(Guid AccountId, Guid AddressId) : Message, IEvent;
}
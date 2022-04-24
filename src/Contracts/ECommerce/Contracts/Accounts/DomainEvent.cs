using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Accounts;

public static class DomainEvent
{
    public record AccountDeleted(Guid AccountId) : Message, IEvent;

    public record AccountCreated(Guid AccountId, Guid UserId, string Email, string FirstName) : Message, IEvent;

    public record ProfileUpdated(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Message, IEvent;

    public record ResidenceAddressDefined(Guid AccountId, Models.Address Address) : Message, IEvent;

    public record ProfessionalAddressDefined(Guid AccountId, Models.Address Address) : Message, IEvent;
}
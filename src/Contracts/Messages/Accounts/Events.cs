using System;
using Messages.Abstractions;

namespace Messages.Accounts
{
    public static class Events
    {
        public record AccountDeleted(Guid Id) : Message<Guid>(Id), IEvent;

        public record AccountCreated(Guid Id, Guid UserId, string Email, string FirstName) : Message<Guid>(Id), IEvent;

        public record ProfileUpdated(Guid Id, DateOnly Birthdate, string Email, string FirstName, string LastName) : Message<Guid>(Id), IEvent;

        public record ResidenceAddressDefined(Guid Id, string City, string Country, int? Number, string State, string Street, string ZipCode) : Message<Guid>(Id), IEvent;

        public record ProfessionalAddressDefined(Guid Id, string City, string Country, int? Number, string State, string Street, string ZipCode) : Message<Guid>(Id), IEvent;
    }
}
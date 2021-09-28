using System;
using Messages.Abstractions.Events;

namespace Messages.Accounts
{
    public static class Events
    {
        public record AccountDeleted(Guid Id) : Event;

        public record AccountCreated(Guid Id, Guid UserId, string Email, string FirstName) : Event;

        public record ProfileUpdated(Guid Id, DateOnly Birthdate, string Email, string FirstName, string LastName) : Event;

        public record ResidenceAddressDefined(Guid Id, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;

        public record ProfessionalAddressDefined(Guid Id, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;
    }
}
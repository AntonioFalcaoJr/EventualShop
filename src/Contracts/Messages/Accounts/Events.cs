using System;
using Messages.Abstractions.Events;

namespace Messages.Accounts
{
    public static class Events
    {
        public record AccountDeleted(Guid AccountId) : Event;

        public record AccountCreated(Guid AccountId, Guid UserId, string Email, string FirstName) : Event;

        public record ProfileUpdated(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Event;

        public record ResidenceAddressDefined(Guid AccountId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;

        public record ProfessionalAddressDefined(Guid AccountId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;
    }
}
using System;
using Messages.Abstractions;

namespace Messages.Accounts
{
    public static class Commands
    {
        public record CreateAccount(Guid AccountId, string Email, string FirstName) : Message<Guid>(AccountId), ICommand;

        public record DefineProfessionalAddress(Guid AccountId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Message<Guid>(AccountId), ICommand;

        public record DefineResidenceAddress(Guid AccountId, Guid OwnerId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Message<Guid>(AccountId), ICommand;

        public record DeleteAccount(Guid AccountId) : Message<Guid>(AccountId), ICommand;

        public record UpdateProfile(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Message<Guid>(AccountId), ICommand;
    }
}
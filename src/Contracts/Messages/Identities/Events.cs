using System;
using Messages.Abstractions;

namespace Messages.Identities
{
    public static class Events
    {
        public record UserDeleted(Guid Id) : Message<Guid>(Id), IEvent;

        public record UserRegistered(Guid Id, string Password, string PasswordConfirmation, string Login) : Message<Guid>(Id), IEvent;

        public record UserPasswordChanged(Guid Id, string NewPassword, string NewPasswordConfirmation) : Message<Guid>(Id), IEvent;
    }
}
using System;
using Messages.Abstractions;

namespace Messages.Identities
{
    public static class Events
    {
        public record UserDeleted(Guid UserId) : Message<Guid>(UserId), IEvent;

        public record UserRegistered(Guid UserId, string Email, string FirstName, string PasswordConfirmation, string Login) : Message<Guid>(UserId), IEvent;

        public record UserPasswordChanged(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message<Guid>(UserId), IEvent;
    }
}
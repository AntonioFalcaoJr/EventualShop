using System;
using Messages.Abstractions.Events;

namespace Messages.Identities
{
    public static class Events
    {
        public record UserDeleted(Guid UserId) : Event;

        public record UserRegistered(Guid UserId, string Email, string FirstName, string Password, string PasswordConfirmation) : Event;

        public record UserPasswordChanged(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Event;
    }
}
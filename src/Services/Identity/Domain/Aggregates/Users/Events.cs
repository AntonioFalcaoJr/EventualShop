using System;
using Domain.Abstractions.Events;
using Messages.Identities.Events;

namespace Domain.Aggregates.Users
{
    public static class Events
    {
        public record UserDeleted(Guid UserId) : DomainEvent;

        public record UserRegistered(Guid UserId, string Password, string PasswordConfirmation, string Login) : DomainEvent;

        public record UserPasswordChanged(Guid UserId, string NewPassword, string NewPasswordConfirmation) : DomainEvent;
    }
}
using System;
using Domain.Abstractions.Aggregates;
using Messages.Abstractions;
using Messages.Identities;

namespace Domain.Aggregates.Users
{
    public class User : AggregateRoot<Guid>
    {
        public string Password { get; private set; }
        public string PasswordConfirmation { get; private set; }
        public string Login { get; private set; }

        public void Register(string password, string passwordConfirmation, string login)
            => RaiseEvent(new Events.UserRegistered(Guid.NewGuid(), password, passwordConfirmation, login));

        public void ChangePassword(Guid userId, string newPassword, string newPasswordConfirmation)
            => RaiseEvent(new Events.UserPasswordChanged(userId, newPassword, newPasswordConfirmation));

        public void Delete(Guid userId)
            => RaiseEvent(new Events.UserDeleted(userId));

        protected override void ApplyEvent(IEvent domainEvent)
            => When(domainEvent as dynamic);

        private void When(Events.UserRegistered @event)
            => (Id, Password, PasswordConfirmation, Login) = @event;

        private void When(Events.UserPasswordChanged @event)
            => (_, Password, PasswordConfirmation) = @event;

        private void When(Events.UserDeleted _)
            => IsDeleted = true;

        protected sealed override bool Validate()
            => OnValidate<UserValidator, User>();
    }
}
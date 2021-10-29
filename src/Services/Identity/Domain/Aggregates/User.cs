using System;
using Domain.Abstractions.Aggregates;
using Messages.Abstractions.Events;
using Messages.Identities;

namespace Domain.Aggregates;

public class User : AggregateRoot<Guid>
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirmation { get; private set; }

    public void Register(string email, string firstName, string password, string passwordConfirmation)
        => RaiseEvent(new Events.UserRegistered(Guid.NewGuid(), email, firstName, password, passwordConfirmation));

    public void ChangePassword(Guid userId, string newPassword, string newPasswordConfirmation)
        => RaiseEvent(new Events.UserPasswordChanged(userId, newPassword, newPasswordConfirmation));

    public void Delete(Guid userId)
        => RaiseEvent(new Events.UserDeleted(userId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(Events.UserRegistered @event)
        => (Id, Email, FirstName, Password, PasswordConfirmation) = @event;

    private void When(Events.UserPasswordChanged @event)
        => (_, Password, PasswordConfirmation) = @event;

    private void When(Events.UserDeleted _)
        => IsDeleted = true;

    protected sealed override bool Validate()
        => OnValidate<UserValidator, User>();
}
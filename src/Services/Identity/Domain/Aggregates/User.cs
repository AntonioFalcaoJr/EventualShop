using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions;
using ECommerce.Contracts.Identities;

namespace Domain.Aggregates;

public class User : AggregateRoot<Guid, UserValidator>
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirmation { get; private set; }

    public void Register(string email, string firstName, string password, string passwordConfirmation)
        => RaiseEvent(new DomainEvent.UserRegistered(Guid.NewGuid(), email, firstName, password, passwordConfirmation));

    public void ChangePassword(Guid userId, string newPassword, string newPasswordConfirmation)
        => RaiseEvent(new DomainEvent.UserPasswordChanged(userId, newPassword, newPasswordConfirmation));

    public void Delete(Guid userId)
        => RaiseEvent(new DomainEvent.UserDeleted(userId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.UserRegistered @event)
        => (Id, Email, FirstName, Password, PasswordConfirmation) = @event;

    private void When(DomainEvent.UserPasswordChanged @event)
        => (_, Password, PasswordConfirmation) = @event;

    private void When(DomainEvent.UserDeleted _)
        => IsDeleted = true;
}
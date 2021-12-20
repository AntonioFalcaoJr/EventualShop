using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Identity;

namespace Domain.Aggregates;

public class User : AggregateRoot<Guid>
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirmation { get; private set; }

    public void Register(string email, string firstName, string password, string passwordConfirmation)
        => RaiseEvent(new DomainEvents.UserRegistered(Guid.NewGuid(), email, firstName, password, passwordConfirmation));

    public void ChangePassword(Guid userId, string newPassword, string newPasswordConfirmation)
        => RaiseEvent(new DomainEvents.UserPasswordChanged(userId, newPassword, newPasswordConfirmation));

    public void Delete(Guid userId)
        => RaiseEvent(new DomainEvents.UserDeleted(userId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.UserRegistered @event)
        => (Id, Email, FirstName, Password, PasswordConfirmation) = @event;

    private void When(DomainEvents.UserPasswordChanged @event)
        => (_, Password, PasswordConfirmation) = @event;

    private void When(DomainEvents.UserDeleted _)
        => IsDeleted = true;

    protected sealed override bool Validate()
        => OnValidate<UserValidator, User>();
}
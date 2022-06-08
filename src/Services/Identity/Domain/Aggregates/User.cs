using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Identity;

namespace Domain.Aggregates;

public class User : AggregateRoot<Guid, UserValidator>
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirmation { get; private set; }

    public void Handle(Command.Register cmd)
        => RaiseEvent(new DomainEvent.UserRegistered(Guid.NewGuid(), cmd.Email, cmd.Password, cmd.PasswordConfirmation));

    public void Handle(Command.ChangePassword cmd)
        => RaiseEvent(new DomainEvent.UserPasswordChanged(cmd.UserId, cmd.NewPassword, cmd.NewPasswordConfirmation));

    public void Handle(Command.Delete cmd)
        => RaiseEvent(new DomainEvent.UserDeleted(cmd.UserId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.UserRegistered @event)
        => (Id, Email, Password, PasswordConfirmation) = @event;

    private void When(DomainEvent.UserPasswordChanged @event)
        => (_, Password, PasswordConfirmation) = @event;

    private void When(DomainEvent.UserDeleted _)
        => IsDeleted = true;
}
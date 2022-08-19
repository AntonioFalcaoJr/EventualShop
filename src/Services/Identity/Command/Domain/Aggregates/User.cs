using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Identity;

namespace Domain.Aggregates;

public class User : AggregateRoot<UserValidator>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirmation { get; private set; }

    public override void Handle(ICommandWithId command)
        => Handle(command as dynamic);

    private void Handle(Command.RegisterUser cmd)
    {
        if (Id != cmd.Id)
            RaiseEvent(new DomainEvent.UserRegistered(
                cmd.Id, cmd.FirstName, cmd.LastName, cmd.Email, cmd.Password, cmd.PasswordConfirmation));
    }

    private void Handle(Command.ChangePassword cmd)
        => RaiseEvent(new DomainEvent.UserPasswordChanged(cmd.Id, cmd.NewPassword, cmd.NewPasswordConfirmation));

    private void Handle(Command.DeleteUser cmd)
        => RaiseEvent(new DomainEvent.UserDeleted(cmd.Id));

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.UserRegistered @event)
        => (Id, FirstName, LastName, Email, Password, PasswordConfirmation) = @event;

    private void Apply(DomainEvent.UserPasswordChanged @event)
        => (_, Password, PasswordConfirmation) = @event;

    private void Apply(DomainEvent.UserDeleted _)
        => IsDeleted = true;
}
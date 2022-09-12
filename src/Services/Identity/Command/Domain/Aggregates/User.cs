using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Identity;
using Domain.Enumerations;
using Domain.ValueObjects.Emails;

namespace Domain.Aggregates;

public class User : AggregateRoot<UserValidator>
{
    private readonly List<Email> _emails = new();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }

    public IEnumerable<Email> Emails
        => _emails;

    public override void Handle(ICommandWithId command)
        => Handle(command as dynamic);

    private void Handle(Command.RegisterUser cmd)
        => RaiseEvent(new DomainEvent.UserRegistered(cmd.Id, cmd.FirstName, cmd.LastName, cmd.Email, cmd.Password));

    private void Handle(Command.ChangePassword cmd)
        => RaiseEvent(new DomainEvent.PasswordChanged(cmd.Id, cmd.NewPassword));

    private void Handle(Command.DeleteUser cmd)
        => RaiseEvent(new DomainEvent.UserDeleted(cmd.Id));

    private void Handle(Command.ConfirmEmail cmd)
        => RaiseEvent(new DomainEvent.EmailConfirmed(cmd.Id, cmd.Email));

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.UserRegistered @event)
    {
        (Id, FirstName, LastName, var email, Password) = @event;
        _emails.Add(email);
    }

    private void Apply(DomainEvent.PasswordChanged @event)
        => Password = @event.NewPassword;

    private void Apply(DomainEvent.UserDeleted _)
        => IsDeleted = true;

    private void Apply(DomainEvent.EmailConfirmed @event)
    {
        var indexOf = _emails.IndexOf(@event.Email);
        _emails[indexOf] = new(@event.Email, EmailStatus.Verified);
    }
}
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
    public string PrimaryEmail { get; private set; }

    public IEnumerable<Email> Emails
        => _emails;

    public override void Handle(ICommandWithId command)
        => Handle(command as dynamic);

    private void Handle(Command.RegisterUser cmd)
        => RaiseEvent(new DomainEvent.UserRegistered(cmd.Id, cmd.FirstName, cmd.LastName, cmd.Email, cmd.Password));

    private void Handle(Command.ChangePassword cmd)
    {
        if (cmd.NewPassword == Password) return;
        RaiseEvent(new DomainEvent.PasswordChanged(cmd.Id, cmd.NewPassword));
    }

    private void Handle(Command.DeleteUser cmd)
    {
        if (IsDeleted) return;
        RaiseEvent(new DomainEvent.UserDeleted(cmd.Id));
    }

    private void Handle(Command.ConfirmEmail cmd)
    {
        if (_emails.Exists(email => email == cmd.Email && email.Status == EmailStatus.Unverified))
            RaiseEvent(new DomainEvent.EmailConfirmed(cmd.Id, cmd.Email));
    }

    private void Handle(Command.DefinePrimaryEmail cmd)
    {
        if (cmd.Email == PrimaryEmail) return;
        RaiseEvent(new DomainEvent.PrimaryEmailDefined(cmd.Id, cmd.Email));
    }

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

    private void Apply(DomainEvent.PrimaryEmailDefined @event)
        => PrimaryEmail = @event.Email;
}
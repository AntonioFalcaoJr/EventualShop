using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Identity;
using Domain.Enumerations;
using Domain.ValueObjects.Emails;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class User : AggregateRoot<UserValidator>
{
    [JsonProperty]
    private readonly List<Email> _emails = new();

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Password { get; private set; }
    public string? PrimaryEmail { get; private set; }

    public IEnumerable<Email> Emails
        => _emails.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.RegisterUser cmd)
        => RaiseEvent<DomainEvent.UserRegistered>(version
            => new(cmd.UserId, cmd.FirstName, cmd.LastName, cmd.Email, cmd.Password, version));

    private void Handle(Command.ConfirmEmail cmd)
    {
        if (_emails.SingleOrDefault(email => email == cmd.Email) is not { Status: EmailStatus.VerifiedStatus }) return;
        RaiseEvent<DomainEvent.EmailConfirmed>(version => new(cmd.UserId, cmd.Email, version));
    }

    private void Handle(Command.ChangeEmail cmd)
    {
        if (cmd.Email.Equals(PrimaryEmail, StringComparison.OrdinalIgnoreCase)) return;
        RaiseEvent<DomainEvent.EmailChanged>(version => new(cmd.UserId, cmd.Email, version));
    }

    private void Handle(Command.ChangePassword cmd)
    {
        if (cmd.NewPassword == Password) return;
        RaiseEvent<DomainEvent.UserPasswordChanged>(version => new(cmd.UserId, cmd.NewPassword, version));
    }

    private void Handle(Command.DeleteUser cmd)
    {
        if (IsDeleted) return;
        RaiseEvent<DomainEvent.UserDeleted>(version => new(cmd.UserId, version));
    }

    private void Handle(Command.ExpiryEmail cmd)
    {
        if (_emails.SingleOrDefault(email => email == cmd.Email) is not { Status: EmailStatus.VerifiedStatus }) return;
        RaiseEvent<DomainEvent.EmailExpired>(version => new(cmd.UserId, cmd.Email, version));
    }

    private void Handle(Command.DefinePrimaryEmail cmd)
    {
        if (cmd.Email == PrimaryEmail) return;
        RaiseEvent<DomainEvent.PrimaryEmailDefined>(version => new(cmd.UserId, cmd.Email, version));
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.UserRegistered @event)
    {
        (Id, FirstName, LastName, var email, Password, _) = @event;
        _emails.Add(email);
    }

    private void When(DomainEvent.UserPasswordChanged @event)
        => Password = @event.Password;

    private void When(DomainEvent.UserDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.EmailChanged @event)
        => _emails.Add(@event.Email);

    private void When(DomainEvent.EmailConfirmed @event)
    {
        var (email, index) = _emails
            .Where(email => email == @event.Email)
            .Select((email, index) => (email, index))
            .First();

        _emails[index] = email with { Status = EmailStatus.Verified };
    }

    private void When(DomainEvent.EmailExpired @event)
    {
        var (email, index) = _emails
            .Where(email => email == @event.Email)
            .Select((email, index) => (email, index))
            .First();

        _emails[index] = email with { Status = EmailStatus.Expired };
    }

    private void When(DomainEvent.PrimaryEmailDefined @event)
        => PrimaryEmail = @event.Email;
}
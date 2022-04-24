using Domain.Abstractions.Aggregates;
using Domain.ValueObjects.Profiles;
using ECommerce.Abstractions;
using ECommerce.Contracts.Accounts;

namespace Domain.Aggregates;

public class Account : AggregateRoot<Guid, AccountValidator>
{
    public Guid UserId { get; private set; }
    public Profile Profile { get; private set; }

    public void Handle(Command.CreateAccount cmd)
        => RaiseEvent(new DomainEvent.AccountCreated(Guid.NewGuid(), cmd.UserId, cmd.Email, cmd.FirstName));

    public void Handle(Command.DeleteAccount cmd)
        => RaiseEvent(new DomainEvent.AccountDeleted(cmd.AccountId));

    public void Handle(Command.UpdateProfile cmd)
        => RaiseEvent(new DomainEvent.ProfileUpdated(cmd.AccountId, cmd.Birthdate, cmd.Email, cmd.FirstName, cmd.LastName));

    public void Handle(Command.DefineResidenceAddress cmd)
        => RaiseEvent(new DomainEvent.ResidenceAddressDefined(cmd.AccountId, cmd.Address));

    public void Handle(Command.DefineProfessionalAddress cmd)
        => RaiseEvent(new DomainEvent.ProfessionalAddressDefined(cmd.AccountId, cmd.Address));

    protected override void ApplyEvent(IEvent domainEvent)
        => When(domainEvent as dynamic);

    private void When(DomainEvent.AccountCreated @event)
    {
        Id = @event.AccountId;
        UserId = @event.UserId;
        Profile = new(@event.Email, @event.FirstName);
    }

    private void When(DomainEvent.AccountDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.ProfileUpdated @event)
    {
        Profile.SetBirthdate(@event.Birthdate);
        Profile.SetEmail(@event.Email);
        Profile.SetFirstName(@event.FirstName);
        Profile.SetLastName(@event.LastName);
    }

    private void When(DomainEvent.ResidenceAddressDefined @event)
        => Profile.SetResidenceAddress(new(
            @event.Address.City,
            @event.Address.Country,
            @event.Address.Number,
            @event.Address.State,
            @event.Address.Street,
            @event.Address.ZipCode));

    private void When(DomainEvent.ProfessionalAddressDefined @event)
        => Profile.SetProfessionalAddress(new(
            @event.Address.City,
            @event.Address.Country,
            @event.Address.Number,
            @event.Address.State,
            @event.Address.Street,
            @event.Address.ZipCode));
}
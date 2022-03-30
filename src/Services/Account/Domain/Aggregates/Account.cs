using Domain.Abstractions.Aggregates;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.Profiles;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Account;

namespace Domain.Aggregates;

public class Account : AggregateRoot<Guid, AccountValidator>
{
    public Guid UserId { get; private set; }
    public Profile Profile { get; private set; }

    public void Handle(Commands.CreateAccount cmd)
        => RaiseEvent(new DomainEvents.AccountCreated(Guid.NewGuid(), cmd.UserId, cmd.Email, cmd.FirstName));

    public void Handle(Commands.DeleteAccount cmd)
        => RaiseEvent(new DomainEvents.AccountDeleted(cmd.AccountId));

    public void Handle(Commands.UpdateProfile cmd)
        => RaiseEvent(new DomainEvents.ProfileUpdated(cmd.AccountId, cmd.Birthdate, cmd.Email, cmd.FirstName, cmd.LastName));

    public void Handle(Commands.DefineResidenceAddress cmd)
        => RaiseEvent(new DomainEvents.ResidenceAddressDefined(cmd.AccountId, cmd.City, cmd.Country, cmd.Number, cmd.State, cmd.Street, cmd.ZipCode));

    public void Handle(Commands.DefineProfessionalAddress cmd)
        => RaiseEvent(new DomainEvents.ProfessionalAddressDefined(cmd.AccountId, cmd.City, cmd.Country, cmd.Number, cmd.State, cmd.Street, cmd.ZipCode));

    protected override void ApplyEvent(IEvent domainEvent)
        => When(domainEvent as dynamic);

    private void When(DomainEvents.AccountCreated @event)
    {
        Id = @event.AccountId;
        UserId = @event.UserId;
        Profile = new(@event.Email, @event.FirstName);
    }

    private void When(DomainEvents.AccountDeleted _)
        => IsDeleted = true;

    private void When(DomainEvents.ProfileUpdated @event)
    {
        Profile.SetBirthdate(@event.Birthdate);
        Profile.SetEmail(@event.Email);
        Profile.SetFirstName(@event.FirstName);
        Profile.SetLastName(@event.LastName);
    }

    private void When(DomainEvents.ResidenceAddressDefined @event)
    {
        var address = new Address(
            @event.City,
            @event.Country,
            @event.Number,
            @event.State,
            @event.Street,
            @event.ZipCode);

        Profile.SetResidenceAddress(address);
    }

    private void When(DomainEvents.ProfessionalAddressDefined @event)
    {
        var address = new Address(
            @event.City,
            @event.Country,
            @event.Number,
            @event.State,
            @event.Street,
            @event.ZipCode);

        Profile.SetProfessionalAddress(address);
    }
}
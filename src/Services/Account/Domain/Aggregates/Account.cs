using Domain.Abstractions.Aggregates;
using Domain.ValueObjects.Profiles;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Accounts;

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
        => RaiseEvent(new DomainEvents.ResidenceAddressDefined(cmd.AccountId, cmd.Address));

    public void Handle(Commands.DefineProfessionalAddress cmd)
        => RaiseEvent(new DomainEvents.ProfessionalAddressDefined(cmd.AccountId, cmd.Address));

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
        => Profile.SetResidenceAddress(new(
            @event.Address.City,
            @event.Address.Country,
            @event.Address.Number,
            @event.Address.State,
            @event.Address.Street,
            @event.Address.ZipCode));

    private void When(DomainEvents.ProfessionalAddressDefined @event)
        => Profile.SetProfessionalAddress(new(
            @event.Address.City,
            @event.Address.Country,
            @event.Address.Number,
            @event.Address.State,
            @event.Address.Street,
            @event.Address.ZipCode));
}
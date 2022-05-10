using Domain.Abstractions.Aggregates;
using Contracts.Abstractions;
using Contracts.Services.Account;
using Domain.Entities.Profiles;
using Domain.ValueObjects.Addresses;

namespace Domain.Aggregates;

public class Account : AggregateRoot<Guid, AccountValidator>
{
    private readonly List<Address> _addresses = new();

    public Profile Profile { get; private set; }

    // public Wallet Wallet { get; private set; }
    public bool WishToReceiveNews { get; private set; }
    public bool AcceptedPolicies { get; private set; }
    public string Password { get; private set; }

    public IEnumerable<Address> Addresses
        => _addresses;

    public void Handle(Command.CreateAccount cmd)
        => RaiseEvent(new DomainEvent.AccountCreated(
            Guid.NewGuid(),
            cmd.Profile,
            cmd.Password,
            cmd.PasswordConfirmation,
            cmd.AcceptedPolicies,
            cmd.WishToReceiveNews));

    public void Handle(Command.DeleteAccount cmd)
        => RaiseEvent(new DomainEvent.AccountDeleted(cmd.AccountId));

    public void Handle(Command.AddBillingAddress cmd)
        => RaiseEvent(new DomainEvent.BillingAddressAdded(cmd.AccountId, cmd.Address));

    public void Handle(Command.AddShippingAddress cmd)
        => RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.AccountId, cmd.Address));

    protected override void ApplyEvent(IEvent domainEvent)
        => When(domainEvent as dynamic);

    private void When(DomainEvent.AccountCreated @event)
        => (Id, Profile, Password, _, AcceptedPolicies, WishToReceiveNews) = @event;

    private void When(DomainEvent.AccountDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.BillingAddressAdded @event)
        => _addresses.Add((BillingAddress) @event.Address);

    private void When(DomainEvent.ShippingAddressAdded @event)
        => _addresses.Add((ShippingAddress) @event.Address);
}
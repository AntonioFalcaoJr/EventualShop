using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Account;
using Domain.Entities.Addresses;
using Domain.Entities.Profiles;

namespace Domain.Aggregates;

public class Account : AggregateRoot<Guid, AccountValidator>
{
    private readonly List<Address> _addresses = new();

    public Profile Profile { get; private set; }

    // TODO - Implement Wallet capability. Interesting in Payment Methods business facts
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
    {
        if (_addresses.OfType<BillingAddress>().All(address
                => address != cmd.Address && address.IsDeleted is false))
            RaiseEvent(new DomainEvent.BillingAddressAdded(cmd.AccountId, Guid.NewGuid(), cmd.Address));
    }

    public void Handle(Command.AddShippingAddress cmd)
    {
        if (_addresses.OfType<ShippingAddress>().All(address
                => address != cmd.Address && address.IsDeleted is false))
            RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.AccountId, Guid.NewGuid(), cmd.Address));
    }

    public void Handle(Command.PreferBillingAddress cmd)
    {
        if (_addresses.OfType<BillingAddress>().SingleOrDefault(address
                => address.Id != cmd.AddressId) is {IsDeleted: false} and {IsPreferred: false})
            RaiseEvent(new DomainEvent.BillingAddressPreferred(cmd.AccountId, cmd.AddressId));
    }

    public void Handle(Command.PreferShippingAddress cmd)
    {
        if (_addresses.OfType<ShippingAddress>().SingleOrDefault(address
                => address.Id != cmd.AddressId) is {IsDeleted: false} and {IsPreferred: false})
            RaiseEvent(new DomainEvent.ShippingAddressPreferred(cmd.AccountId, cmd.AddressId));
    }

    protected override void ApplyEvent(IEvent domainEvent) 
        => When(domainEvent as dynamic);

    private void When(DomainEvent.AccountCreated @event)
        => (Id, Profile, Password, _, AcceptedPolicies, WishToReceiveNews) = @event;

    private void When(DomainEvent.BillingAddressAdded @event)
        => _addresses.Add(BillingAddress.Create(@event.AddressId, @event.Address));

    private void When(DomainEvent.ShippingAddressAdded @event)
        => _addresses.Add(ShippingAddress.Create(@event.AddressId, @event.Address));

    private void When(DomainEvent.BillingAddressPreferred @event)
    {
        foreach (var address in _addresses.OfType<BillingAddress>())
            if (address.Id == @event.AddressId) address.Prefer();
            else address.Unprefer();
    }

    private void When(DomainEvent.ShippingAddressPreferred @event)
    {
        foreach (var address in _addresses.OfType<ShippingAddress>())
            if (address.Id == @event.AddressId) address.Prefer();
            else address.Unprefer();
    }

    private void When(DomainEvent.AccountDeleted _)
        => IsDeleted = true;
}
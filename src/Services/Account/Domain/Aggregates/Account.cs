using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Account;
using Domain.Entities.Addresses;
using Domain.Entities.Profiles;

namespace Domain.Aggregates;

public class Account : AggregateRoot<Guid, AccountValidator>
{
    private readonly List<Address> _addresses = new();

    // TODO - Implement Wallet capability. Interesting in Payment Methods business facts
    // public Wallet Wallet { get; private set; }

    public Profile Profile { get; private set; }
    public bool WishToReceiveNews { get; private set; }
    public bool AcceptedPolicies { get; private set; }

    public IEnumerable<Address> Addresses
        => _addresses;

    public void Handle(Command.CreateAccount cmd)
        => RaiseEvent(new DomainEvent.AccountCreated(cmd.Id, cmd.FirstName, cmd.LastName, cmd.Email));

    public void Handle(Command.DeleteAccount cmd)
    {
        if (IsDeleted) return;
        RaiseEvent(new DomainEvent.AccountDeleted(cmd.Id));
    }

    public void Handle(Command.AddBillingAddress cmd)
    {
        var billingAddress = _addresses.OfType<BillingAddress>().SingleOrDefault(address => address == cmd.Address);

        if (billingAddress is null)
            RaiseEvent(new DomainEvent.BillingAddressAdded(cmd.Id, Guid.NewGuid(), cmd.Address));
        else if (billingAddress.IsDeleted)
            RaiseEvent(new DomainEvent.BillingAddressRestored(cmd.Id, billingAddress.Id));
    }

    public void Handle(Command.PreferBillingAddress cmd)
    {
        if (_addresses
                .OfType<BillingAddress>()
                .SingleOrDefault(address => address.Id == cmd.AddressId)
            is { IsDeleted: false }
            and { IsPreferred: false })

            RaiseEvent(new DomainEvent.BillingAddressPreferred(cmd.Id, cmd.AddressId));
    }

    public void Handle(Command.DeleteBillingAddress cmd)
    {
        if (_addresses.OfType<BillingAddress>().SingleOrDefault(address => address.Id == cmd.AddressId) is { IsDeleted: false })
            RaiseEvent(new DomainEvent.BillingAddressDeleted(cmd.Id, cmd.AddressId));
    }

    public void Handle(Command.AddShippingAddress cmd)
    {
        var shippingAddress = _addresses.OfType<ShippingAddress>().SingleOrDefault(address => address == cmd.Address);

        if (shippingAddress is null)
            RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.Id, Guid.NewGuid(), cmd.Address));
        else if (shippingAddress.IsDeleted)
            RaiseEvent(new DomainEvent.ShippingAddressRestored(cmd.Id, shippingAddress.Id));
    }

    public void Handle(Command.PreferShippingAddress cmd)
    {
        if (_addresses
                .OfType<ShippingAddress>()
                .SingleOrDefault(address => address.Id == cmd.AddressId)
            is { IsDeleted: false }
            and { IsPreferred: false })

            RaiseEvent(new DomainEvent.ShippingAddressPreferred(cmd.Id, cmd.AddressId));
    }

    public void Handle(Command.DeleteShippingAddress cmd)
    {
        if (_addresses.OfType<ShippingAddress>().SingleOrDefault(address => address.Id == cmd.AddressId) is { IsDeleted: false })
            RaiseEvent(new DomainEvent.ShippingAddressDeleted(cmd.Id, cmd.AddressId));
    }

    protected override void ApplyEvent(IEvent domainEvent)
        => When(domainEvent as dynamic);

    private void When(DomainEvent.AccountCreated @event)
    {
        Id = @event.Id;
        Profile = new(@event.FirstName, @event.LastName, @event.Email);
    }

    private void When(DomainEvent.BillingAddressAdded @event)
        => _addresses.Add(BillingAddress.Create(@event.AddressId, @event.Address));

    private void When(DomainEvent.BillingAddressRestored @event)
        => _addresses.First(address => address.Id == @event.AddressId).Restore();

    private void When(DomainEvent.ShippingAddressAdded @event)
        => _addresses.Add(ShippingAddress.Create(@event.AddressId, @event.Address));

    private void When(DomainEvent.ShippingAddressRestored @event)
        => _addresses.First(address => address.Id == @event.AddressId).Restore();

    private void When(DomainEvent.BillingAddressPreferred @event)
    {
        _addresses.OfType<BillingAddress>().First(address => address.IsPreferred).Unprefer();
        _addresses.OfType<BillingAddress>().First(address => address.Id == @event.Id).Prefer();
    }

    private void When(DomainEvent.ShippingAddressPreferred @event)
    {
        _addresses.OfType<ShippingAddress>().First(address => address.IsPreferred).Unprefer();
        _addresses.OfType<ShippingAddress>().First(address => address.Id == @event.Id).Prefer();
    }
    
    private void When(DomainEvent.BillingAddressDeleted @event)
        => _addresses.First(address => address.Id == @event.AddressId).Delete();
    
    private void When(DomainEvent.ShippingAddressDeleted @event)
        => _addresses.First(address => address.Id == @event.AddressId).Delete();

    private void When(DomainEvent.AccountDeleted _)
        => IsDeleted = true;
}
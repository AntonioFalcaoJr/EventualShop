using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Account;
using Domain.Entities.Addresses;
using Domain.Entities.Profiles;
using Contracts.DataTransferObjects;

namespace Domain.Aggregates;

public class Account : AggregateRoot<AccountValidator>
{
    private readonly List<Address> _addresses = new();

    // TODO - Implement Wallet capability. Interesting in Payment Methods business facts
    // public Wallet Wallet { get; private set; }
    public Profile? Profile { get; private set; }
    public bool WishToReceiveNews { get; private set; }
    public bool AcceptedPolicies { get; private set; }

    public IEnumerable<Address> Addresses
        => _addresses;

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.CreateAccount cmd)
        => RaiseEvent(new DomainEvent.AccountCreated(Guid.NewGuid(), cmd.FirstName, cmd.LastName, cmd.Email));

    private void Handle(Command.DeleteAccount cmd)
    {
        if (IsDeleted) return;
        RaiseEvent(new DomainEvent.AccountDeleted(cmd.AccountId));
    }

    private void Handle(Command.AddBillingAddress cmd)
    {
        var billingAddress = _addresses.OfType<BillingAddress>().SingleOrDefault(address => address == cmd.Address);

        if (billingAddress is null)
            RaiseEvent(new DomainEvent.BillingAddressAdded(cmd.AccountId, Guid.NewGuid(), cmd.Address));
        else if (billingAddress.IsDeleted)
            RaiseEvent(new DomainEvent.BillingAddressRestored(cmd.AccountId, billingAddress.Id));
    }

    private void Handle(Command.PreferBillingAddress cmd)
    {
        if (_addresses
                .OfType<BillingAddress>()
                .SingleOrDefault(address => address.Id == cmd.AddressId)
            is { IsDeleted: false }
            and { IsPreferred: false })

            RaiseEvent(new DomainEvent.BillingAddressPreferred(cmd.AccountId, cmd.AddressId));
    }

    private void Handle(Command.DeleteBillingAddress cmd)
    {
        if (_addresses.OfType<BillingAddress>().SingleOrDefault(address => address.Id == cmd.AddressId) is { IsDeleted: false })
            RaiseEvent(new DomainEvent.BillingAddressDeleted(cmd.AccountId, cmd.AddressId));
    }

    private void Handle(Command.AddShippingAddress cmd)
    {
        var shippingAddress = _addresses.OfType<ShippingAddress>().SingleOrDefault(address => address == cmd.Address);

        if (shippingAddress is null)
            RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.AccountId, Guid.NewGuid(), cmd.Address));
        else if (shippingAddress.IsDeleted)
            RaiseEvent(new DomainEvent.ShippingAddressRestored(cmd.AccountId, shippingAddress.Id));
    }

    private void Handle(Command.PreferShippingAddress cmd)
    {
        if (_addresses
                .OfType<ShippingAddress>()
                .SingleOrDefault(address => address.Id == cmd.AddressId)
            is { IsDeleted: false }
            and { IsPreferred: false })

            RaiseEvent(new DomainEvent.ShippingAddressPreferred(cmd.AccountId, cmd.AddressId));
    }

    private void Handle(Command.DeleteShippingAddress cmd)
    {
        if (_addresses.OfType<ShippingAddress>().SingleOrDefault(address => address.Id == cmd.AddressId) is { IsDeleted: false })
            RaiseEvent(new DomainEvent.ShippingAddressDeleted(cmd.AccountId, cmd.AddressId));
    }

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.AccountCreated @event)
    {
        Id = @event.AccountId;
        Profile = new(@event.FirstName, @event.LastName, @event.Email);
    }

    private void Apply(DomainEvent.BillingAddressAdded @event)
        => _addresses.Add(BillingAddress.Create(@event.AddressId, @event.Address));

    private void Apply(DomainEvent.BillingAddressRestored @event)
        => _addresses.First(address => address.Id == @event.AddressId).Restore();

    private void Apply(DomainEvent.ShippingAddressAdded @event)
        => _addresses.Add(ShippingAddress.Create(@event.AddressId, @event.Address));

    private void Apply(DomainEvent.ShippingAddressRestored @event)
        => _addresses.First(address => address.Id == @event.AddressId).Restore();

    private void Apply(DomainEvent.BillingAddressPreferred @event)
    {
        _addresses.OfType<BillingAddress>().First(address => address.IsPreferred).Unprefer();
        _addresses.OfType<BillingAddress>().First(address => address.Id == @event.AccountId).Prefer();
    }

    private void Apply(DomainEvent.ShippingAddressPreferred @event)
    {
        _addresses.OfType<ShippingAddress>().First(address => address.IsPreferred).Unprefer();
        _addresses.OfType<ShippingAddress>().First(address => address.Id == @event.AccountId).Prefer();
    }

    private void Apply(DomainEvent.BillingAddressDeleted @event)
        => _addresses.First(address => address.Id == @event.AddressId).Delete();

    private void Apply(DomainEvent.ShippingAddressDeleted @event)
        => _addresses.First(address => address.Id == @event.AddressId).Delete();

    private void Apply(DomainEvent.AccountDeleted _)
        => IsDeleted = true;

    public static implicit operator Dto.Account(Account account)
        => new(account.Profile, account.Addresses.Select(address => (Dto.AddressItem)address),
            account.WishToReceiveNews, account.AcceptedPolicies, account.IsDeleted);
}
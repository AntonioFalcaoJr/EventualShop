using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Account;
using Domain.Entities.Addresses;
using Domain.Entities.Profiles;
using Domain.Enumerations;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class Account : AggregateRoot<AccountValidator>
{
    [JsonProperty]
    private readonly List<Address> _addresses = new();

    // TODO - Implement Wallet capability. Interesting in Payment Methods business facts
    // public Wallet Wallet { get; private set; }

    public bool WishToReceiveNews { get; private set; }
    public bool AcceptedPolicies { get; private set; }
    public Profile? Profile { get; private set; }
    public Address? PrimaryBillingAddress { get; private set; }
    public Address? PrimaryShippingAddress { get; private set; }
    public AccountStatus? Status { get; private set; }

    public IEnumerable<Address> Addresses
        => _addresses.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.CreateAccount cmd)
        => RaiseEvent<DomainEvent.AccountCreated>(version
            => new(Guid.NewGuid(), cmd.FirstName, cmd.LastName, cmd.Email, AccountStatus.Pending, version));

    private void Handle(Command.ActiveAccount cmd)
    {
        if (Status is not AccountStatus.InactiveStatus) return;
        RaiseEvent<DomainEvent.AccountActivated>(version => new(cmd.AccountId, AccountStatus.Active, version));
    }

    private void Handle(Command.DeleteAccount cmd)
    {
        if (IsDeleted) return;
        RaiseEvent<DomainEvent.AccountDeleted>(version => new(cmd.AccountId, AccountStatus.Closed, version));
    }

    private void Handle(Command.AddBillingAddress cmd)
    {
        var billingAddress = _addresses
            .OfType<BillingAddress>()
            .SingleOrDefault(address => address == cmd.Address);

        if (billingAddress is null)
            RaiseEvent<DomainEvent.BillingAddressAdded>(version => new(cmd.AccountId, Guid.NewGuid(), cmd.Address, version));

        else if (billingAddress.IsDeleted)
            RaiseEvent<DomainEvent.BillingAddressRestored>(version => new(cmd.AccountId, billingAddress.Id, version));
    }

    private void Handle(Command.PreferBillingAddress cmd)
    {
        if (_addresses.OfType<BillingAddress>().SingleOrDefault(address
                => address.Id == cmd.AddressId) is not { IsDeleted: false } billingAddress) return;

        if (billingAddress.Id == PrimaryBillingAddress?.Id) return;

        RaiseEvent<DomainEvent.BillingAddressPreferred>(version => new(cmd.AccountId, cmd.AddressId, version));
    }

    private void Handle(Command.DeleteBillingAddress cmd)
    {
        if (_addresses.OfType<BillingAddress>().SingleOrDefault(address
                => address.Id == cmd.AddressId) is not { IsDeleted: false }) return;

        RaiseEvent<DomainEvent.BillingAddressDeleted>(version => new(cmd.AccountId, cmd.AddressId, version));

        if (cmd.AddressId == PrimaryBillingAddress?.Id)
            RaiseEvent<DomainEvent.PrimaryBillingAddressRemoved>(version => new(cmd.AccountId, cmd.AddressId, version));
    }

    private void Handle(Command.AddShippingAddress cmd)
    {
        var shippingAddress = _addresses
            .OfType<ShippingAddress>()
            .SingleOrDefault(address => address == cmd.Address);

        if (shippingAddress is null)
            RaiseEvent<DomainEvent.ShippingAddressAdded>(version => new(cmd.AccountId, Guid.NewGuid(), cmd.Address, version));

        else if (shippingAddress.IsDeleted)
            RaiseEvent<DomainEvent.ShippingAddressRestored>(version => new(cmd.AccountId, shippingAddress.Id, version));
    }

    private void Handle(Command.PreferShippingAddress cmd)
    {
        if (_addresses.OfType<ShippingAddress>().SingleOrDefault(address
                => address.Id == cmd.AddressId) is not { IsDeleted: false } shippingAddress) return;

        if (shippingAddress.Id == PrimaryShippingAddress?.Id) return;

        RaiseEvent<DomainEvent.ShippingAddressPreferred>(version => new(cmd.AccountId, cmd.AddressId, version));
    }

    private void Handle(Command.DeleteShippingAddress cmd)
    {
        if (_addresses.OfType<ShippingAddress>().SingleOrDefault(address
                => address.Id == cmd.AddressId) is not { IsDeleted: false }) return;

        RaiseEvent<DomainEvent.ShippingAddressDeleted>(version => new(cmd.AccountId, cmd.AddressId, version));

        if (cmd.AddressId == PrimaryShippingAddress?.Id)
            RaiseEvent<DomainEvent.PrimaryShippingAddressRemoved>(version => new(cmd.AccountId, cmd.AddressId, version));
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.AccountCreated @event)
    {
        Id = @event.AccountId;
        Profile = new(@event.FirstName, @event.LastName, @event.Email);
        Status = @event.Status;
    }

    private void When(DomainEvent.AccountActivated @event)
        => Status = @event.Status;

    private void When(DomainEvent.BillingAddressAdded @event)
        => _addresses.Add(BillingAddress.Create(@event.AddressId, @event.Address));

    private void When(DomainEvent.ShippingAddressAdded @event)
        => _addresses.Add(ShippingAddress.Create(@event.AddressId, @event.Address));

    private void When(DomainEvent.BillingAddressPreferred @event)
        => PrimaryBillingAddress = _addresses
            .OfType<BillingAddress>()
            .First(address => address.Id == @event.AddressId);

    private void When(DomainEvent.ShippingAddressPreferred @event)
        => PrimaryShippingAddress = _addresses
            .OfType<ShippingAddress>()
            .First(address => address.Id == @event.AddressId);

    private void When(DomainEvent.BillingAddressDeleted @event)
        => _addresses.First(address => address.Id == @event.AddressId).Delete();

    private void When(DomainEvent.ShippingAddressDeleted @event)
        => _addresses.First(address => address.Id == @event.AddressId).Delete();

    private void When(DomainEvent.PrimaryBillingAddressRemoved _)
        => PrimaryBillingAddress = default;

    private void When(DomainEvent.PrimaryShippingAddressRemoved _)
        => PrimaryShippingAddress = default;

    private void When(DomainEvent.BillingAddressRestored @event)
        => _addresses.First(address => address.Id == @event.AddressId).Restore();

    private void When(DomainEvent.ShippingAddressRestored @event)
        => _addresses.First(address => address.Id == @event.AddressId).Restore();

    private void When(DomainEvent.AccountDeleted @event)
    {
        Status = @event.Status;
        IsDeleted = true;
    }
}
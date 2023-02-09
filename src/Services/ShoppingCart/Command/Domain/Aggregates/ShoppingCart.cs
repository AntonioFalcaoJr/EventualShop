using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Domain.ValueObjects;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;
using Domain.ValueObjects.PaymentOptions.PayPals;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class ShoppingCart : AggregateRoot<ShoppingCartValidator>
{
    [JsonProperty]
    private readonly List<CartItem> _items = new();

    [JsonProperty]
    private readonly List<PaymentMethod> _paymentMethods = new();

    public Guid CustomerId { get; private set; }
    public CartStatus Status { get; private set; } = CartStatus.Empty;
    public Address? BillingAddress { get; private set; }
    public Address? ShippingAddress { get; private set; }
    public Money Total { get; private set; } = Money.Zero(Currency.Unknown);
    private bool SameBillingShippingAddress { get; set; } = true;

    public Money TotalPayment
        => Total with { Amount = _paymentMethods.Sum(method => method.Amount) };

    public Money AmountDue
        => Total - TotalPayment;

    public IEnumerable<CartItem> Items
        => _items.AsReadOnly();

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.CreateCart cmd)
        => RaiseEvent<DomainEvent.CartCreated>(version
            => new(Guid.NewGuid(), cmd.CustomerId, Money.Zero(cmd.Currency), CartStatus.Active, version));

    private void Handle(Command.AddCartItem cmd)
        => RaiseEvent(version => _items.SingleOrDefault(cartItem => cartItem.Product == cmd.Product) is { IsDeleted: false } item
            ? new DomainEvent.CartItemIncreased(Id, item.Id, (ushort)(item.Quantity + cmd.Quantity), item.UnitPrice, IncreasedTotal(item.UnitPrice, cmd.Quantity), version)
            : new DomainEvent.CartItemAdded(cmd.CartId, Guid.NewGuid(), cmd.InventoryId, cmd.Product, cmd.Quantity, cmd.UnitPrice, IncreasedTotal(cmd.UnitPrice, cmd.Quantity), version));

    private void Handle(Command.ChangeCartItemQuantity cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;

        if (cmd.NewQuantity > item.Quantity)
            RaiseEvent<DomainEvent.CartItemIncreased>(version
                => new(Id, item.Id, cmd.NewQuantity, item.UnitPrice, IncreasedTotal(item.UnitPrice, cmd.NewQuantity), version));

        else if (cmd.NewQuantity < item.Quantity)
            RaiseEvent<DomainEvent.CartItemDecreased>(version
                => new(Id, item.Id, cmd.NewQuantity, item.UnitPrice, DecreasedTotal(item.UnitPrice, cmd.NewQuantity), version));
    }

    private void Handle(Command.RemoveCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;

        RaiseEvent<DomainEvent.CartItemRemoved>(version
            => new(cmd.CartId, cmd.ItemId, item.UnitPrice, item.Quantity, DecreasedTotal(item.UnitPrice, item.Quantity), version));
    }

    private void Handle(Command.AddCreditCard cmd)
    {
        if (cmd.Amount > AmountDue) return;

        RaiseEvent<DomainEvent.CreditCardAdded>(version
            => new(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.CreditCard, version));
    }

    private void Handle(Command.AddDebitCard cmd)
    {
        if (cmd.Amount > AmountDue) return;

        RaiseEvent<DomainEvent.DebitCardAdded>(version
            => new(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.DebitCard, version));
    }

    private void Handle(Command.AddPayPal cmd)
    {
        if (cmd.Amount > AmountDue) return;

        RaiseEvent<DomainEvent.PayPalAdded>(version
            => new(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.PayPal, version));
    }

    private void Handle(Command.AddShippingAddress cmd)
    {
        if (ShippingAddress == cmd.Address) return;

        RaiseEvent<DomainEvent.ShippingAddressAdded>(version
            => new(cmd.CartId, cmd.Address, version));
    }

    private void Handle(Command.AddBillingAddress cmd)
    {
        if (BillingAddress == cmd.Address) return;

        RaiseEvent<DomainEvent.BillingAddressAdded>(version
            => new(cmd.CartId, cmd.Address, version));
    }

    private void Handle(Command.CheckOutCart cmd)
    {
        if (Status is not CartStatus.ActiveStatus) return;
        if (_items is { Count: 0 } || AmountDue > 0) return;

        RaiseEvent<DomainEvent.CartCheckedOut>(version
            => new(cmd.CartId, CartStatus.CheckedOut, version));
    }

    private void Handle(Command.DiscardCart cmd)
    {
        if (Status is CartStatus.AbandonedStatus) return;

        RaiseEvent<DomainEvent.CartDiscarded>(version
            => new(cmd.CartId, CartStatus.Abandoned, version));
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CartCreated @event)
        => (Id, CustomerId, Total, Status, _) = @event;

    private void When(DomainEvent.CartCheckedOut @event)
        => Status = @event.Status;

    private void When(DomainEvent.CartDiscarded @event)
    {
        Status = @event.Status;
        IsDeleted = true;
    }

    private void When(DomainEvent.CartItemIncreased @event)
    {
        _items
            .First(item => item.Id == @event.ItemId)
            .SetQuantity(@event.NewQuantity);

        Total = @event.NewCartTotal;
    }

    private void When(DomainEvent.CartItemDecreased @event)
    {
        _items
            .First(item => item.Id == @event.ItemId)
            .SetQuantity(@event.NewQuantity);

        Total = @event.NewCartTotal;
    }

    private void When(DomainEvent.CartItemRemoved @event)
    {
        _items.First(item => item.Id == @event.ItemId).Delete();
        Total = @event.NewCartTotal;
    }

    private void When(DomainEvent.CartItemAdded @event)
    {
        _items.Add(new(@event.ItemId, @event.Product, @event.Quantity, @event.UnitPrice));
        Total = @event.NewCartTotal;
    }

    private void When(DomainEvent.CreditCardAdded @event)
        => _paymentMethods.Add(new(@event.MethodId, @event.Amount, (CreditCard)@event.CreditCard));

    private void When(DomainEvent.DebitCardAdded @event)
        => _paymentMethods.Add(new(@event.MethodId, @event.Amount, (DebitCard)@event.DebitCard));

    private void When(DomainEvent.PayPalAdded @event)
        => _paymentMethods.Add(new(@event.MethodId, @event.Amount, (PayPal)@event.PayPal));

    private void When(DomainEvent.BillingAddressAdded @event)
    {
        BillingAddress = @event.Address;

        if (SameBillingShippingAddress)
            ShippingAddress = BillingAddress;
    }

    private void When(DomainEvent.ShippingAddressAdded @event)
    {
        ShippingAddress = @event.Address;
        SameBillingShippingAddress = ShippingAddress == BillingAddress;
    }

    private Money IncreasedTotal(Money unitPrice, int quantity)
        => Total + unitPrice * quantity;

    private Money DecreasedTotal(Money unitPrice, int quantity)
        => Total - unitPrice * quantity;
    
    public static implicit operator Dto.ShoppingCart(ShoppingCart cart)
        => new(cart.Id, cart.CustomerId, cart.Status, cart.BillingAddress, 
            cart.ShippingAddress, cart.Total, cart.TotalPayment, cart.AmountDue, 
            cart.Items.Select(item => (Dto.CartItem)item), 
            cart.PaymentMethods.Select(method => (Dto.PaymentMethod)method));
}
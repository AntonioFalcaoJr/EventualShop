using Contracts.Abstractions.Messages;
using Contracts.Services.ShoppingCart;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
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
    public CartStatus Status { get; private set; }
    public Address? BillingAddress { get; private set; }
    public Address? ShippingAddress { get; private set; }
    public decimal Total { get; private set; }
    private bool BillingShippingSame { get; set; } = true;

    public decimal TotalPayment
        => _paymentMethods.Sum(method => method.Amount);

    public decimal AmountDue
        => Total - TotalPayment;

    public IEnumerable<CartItem> Items
        => _items.AsReadOnly();

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.CreateCart cmd)
        => RaiseEvent(new DomainEvent.CartCreated(cmd.CartId, cmd.CustomerId, CartStatus.Active));

    private void Handle(Command.AddCartItem cmd)
        => RaiseEvent(_items.SingleOrDefault(cartItem => cartItem.Product == cmd.Product) is { IsDeleted: false } item
            ? new DomainEvent.CartItemIncreased(Id, item.Id, (ushort)(item.Quantity + cmd.Quantity), item.UnitPrice, IncreasedTotal(item.UnitPrice, cmd.Quantity))
            : new DomainEvent.CartItemAdded(cmd.CartId, Guid.NewGuid(), cmd.InventoryId, cmd.Product, cmd.Quantity, cmd.UnitPrice, IncreasedTotal(cmd.UnitPrice, cmd.Quantity)));

    private void Handle(Command.ChangeCartItemQuantity cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;

        if (cmd.NewQuantity > item.Quantity)
            RaiseEvent(new DomainEvent.CartItemIncreased(Id, item.Id, cmd.NewQuantity, item.UnitPrice, IncreasedTotal(item.UnitPrice, cmd.NewQuantity)));

        else if (cmd.NewQuantity < item.Quantity)
            RaiseEvent(new DomainEvent.CartItemDecreased(Id, item.Id, cmd.NewQuantity, item.UnitPrice, DecreasedTotal(item.UnitPrice, cmd.NewQuantity)));
    }

    private void Handle(Command.RemoveCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;
        RaiseEvent(new DomainEvent.CartItemRemoved(cmd.CartId, cmd.ItemId, item.UnitPrice, item.Quantity, DecreasedTotal(item.UnitPrice, item.Quantity)));
    }

    private void Handle(Command.AddCreditCard cmd)
    {
        if (cmd.Amount > AmountDue) return;
        RaiseEvent(new DomainEvent.CreditCardAdded(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.CreditCard));
    }

    private void Handle(Command.AddDebitCard cmd)
    {
        if (cmd.Amount > AmountDue) return;
        RaiseEvent(new DomainEvent.DebitCardAdded(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.DebitCard));
    }

    private void Handle(Command.AddPayPal cmd)
    {
        if (cmd.Amount > AmountDue) return;
        RaiseEvent(new DomainEvent.PayPalAdded(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.PayPal));
    }

    private void Handle(Command.AddShippingAddress cmd)
    {
        if (ShippingAddress == cmd.Address) return;
        RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.CartId, cmd.Address));
    }

    private void Handle(Command.AddBillingAddress cmd)
    {
        if (BillingAddress == cmd.Address) return;
        RaiseEvent(new DomainEvent.BillingAddressAdded(cmd.CartId, cmd.Address));
    }

    private void Handle(Command.CheckOutCart cmd)
    {
        if (Status is not CartStatus.ActiveStatus) return;
        if (_items is { Count: 0 } || AmountDue > 0) return;
        RaiseEvent(new DomainEvent.CartCheckedOut(cmd.CartId, CartStatus.CheckedOut));
    }

    private void Handle(Command.DiscardCart cmd)
    {
        if (Status is CartStatus.AbandonedStatus) return;
        RaiseEvent(new DomainEvent.CartDiscarded(cmd.CartId, CartStatus.Abandoned));
    }

    protected override void Apply(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CartCreated @event)
        => (Id, CustomerId, Status) = @event;

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

        if (BillingShippingSame)
            ShippingAddress = BillingAddress;
    }

    private void When(DomainEvent.ShippingAddressAdded @event)
    {
        ShippingAddress = @event.Address;
        BillingShippingSame = ShippingAddress == BillingAddress;
    }

    private decimal IncreasedTotal(decimal unitPrice, int quantity)
        => Total + unitPrice * quantity;

    private decimal DecreasedTotal(decimal unitPrice, int quantity)
        => Total - unitPrice * quantity;
}
using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;
using Domain.ValueObjects.PaymentOptions.PayPals;

namespace Domain.Aggregates;

public class ShoppingCart : AggregateRoot<Guid, ShoppingCartValidator>
{
    private readonly List<CartItem> _items = new();
    private readonly List<PaymentMethod> _paymentMethods = new();

    public Guid CustomerId { get; private set; }
    public CartStatus Status { get; private set; }
    public Address BillingAddress { get; private set; }
    public Address ShippingAddress { get; private set; }
    private bool ShippingAndBillingAddressesAreSame { get; set; } = true;

    public decimal Total
        => Items.Sum(item => item.UnitPrice * item.Quantity);

    public decimal TotalPayment
        => PaymentMethods.Sum(method => method.Amount);

    public decimal AmountDue
        => Total - TotalPayment;

    public IEnumerable<CartItem> Items
        => _items;

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods;

    public void Handle(Command.CreateCart cmd)
        => RaiseEvent(new DomainEvent.CartCreated(Guid.NewGuid(), cmd.CustomerId, CartStatus.Confirmed));

    public void Handle(Command.AddCartItem cmd)
        => RaiseEvent(_items
            .Where(inventoryItem => inventoryItem.Product == cmd.Product)
            .SingleOrDefault(inventoryItem => inventoryItem.UnitPrice == cmd.UnitPrice) is {IsDeleted: false} item
            ? new DomainEvent.CartItemIncreased(Id, item.Id, item.UnitPrice)
            : new DomainEvent.CartItemAdded(cmd.CartId, Guid.NewGuid(), cmd.InventoryId, cmd.CatalogId, cmd.Product, cmd.Quantity, cmd.Sku, cmd.UnitPrice));

    public void Handle(Command.IncreaseCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemIncreased(cmd.CartId, cmd.ItemId, item.UnitPrice));
    }

    public void Handle(Command.DecreaseCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemDecreased(cmd.CartId, cmd.ItemId, item.UnitPrice));
    }

    public void Handle(Command.RemoveCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemRemoved(cmd.CartId, cmd.ItemId, item.UnitPrice, item.Quantity));
    }

    public void Handle(Command.AddPaymentMethod cmd)
    {
        if (AmountDue >= cmd.Amount)
            RaiseEvent(new DomainEvent.PaymentMethodAdded(cmd.CartId, Guid.NewGuid(), cmd.Amount, cmd.Option));
    }

    public void Handle(Command.AddShippingAddress cmd)
    {
        if (ShippingAddress != cmd.Address)
            RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.CartId, cmd.Address));
    }

    public void Handle(Command.ChangeBillingAddress cmd)
    {
        if (BillingAddress != cmd.Address)
            RaiseEvent(new DomainEvent.BillingAddressChanged(cmd.CartId, cmd.Address));
    }

    public void Handle(Command.ConfirmCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Sku == cmd.Sku) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemConfirmed(cmd.CartId, item.Id, item.CatalogId, cmd.Sku, cmd.Quantity));
    }

    public void Handle(Command.CheckOutCart cmd)
    {
        if (_items.Any() && AmountDue is 0)
            RaiseEvent(new DomainEvent.CartCheckedOut(cmd.CartId));
    }

    public void Handle(Command.DiscardCart cmd)
        => RaiseEvent(new DomainEvent.CartDiscarded(cmd.CartId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CartCreated @event)
        => (Id, CustomerId, Status) = @event;

    private void When(DomainEvent.CartCheckedOut _)
        => Status = CartStatus.CheckedOut;

    private void When(DomainEvent.CartDiscarded _)
    {
        Status = CartStatus.Abandoned;
        IsDeleted = true;
    }

    private void When(DomainEvent.CartItemIncreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Increase();

    private void When(DomainEvent.CartItemDecreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Decrease();

    private void When(DomainEvent.CartItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);

    private void When(DomainEvent.CartItemAdded @event)
        => _items.Add(new(@event.ItemId, @event.CatalogId, @event.Product, @event.Quantity, @event.Sku, @event.UnitPrice));

    private void When(DomainEvent.CartItemConfirmed @event)
        => _items.Single(item => item.Id == @event.ItemId).Confirm();

    private void When(DomainEvent.PaymentMethodAdded @event)
        => _paymentMethods.Add(new(@event.MethodId, @event.Amount, @event.Option switch
        {
            Dto.CreditCard creditCard => (CreditCard) creditCard,
            Dto.DebitCard debitCard => (DebitCard) debitCard,
            Dto.PayPal payPal => (PayPal) payPal,
            _ => default
        }));

    private void When(DomainEvent.BillingAddressChanged @event)
    {
        ShippingAddress = @event.Address;

        if (ShippingAndBillingAddressesAreSame)
            BillingAddress = ShippingAddress;
    }

    private void When(DomainEvent.ShippingAddressAdded @event)
    {
        BillingAddress = @event.Address;
        ShippingAndBillingAddressesAreSame = false;
    }
}
using System.Collections.ObjectModel;
using Contracts.Abstractions.Messages;
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
    {
        if (_items.Exists(cartItem => cartItem.Id == cmd.ItemId)) return;

        RaiseEvent(_items.SingleOrDefault(cartItem => cartItem.Product == cmd.Product) is { IsDeleted: false } item
            ? new DomainEvent.CartItemIncreased(Id, item.Id, cmd.Quantity, item.UnitPrice)
            : new DomainEvent.CartItemAdded(cmd.Id, cmd.ItemId, cmd.InventoryId, cmd.CatalogId, cmd.Product, cmd.Quantity, cmd.UnitPrice));
    }

    public void Handle(Command.ChangeCartItemQuantity cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;

        if (cmd.Quantity > item.Quantity)
            RaiseEvent(new DomainEvent.CartItemIncreased(Id, item.Id, cmd.Quantity, item.UnitPrice));

        else if (cmd.Quantity < item.Quantity)
            RaiseEvent(new DomainEvent.CartItemDecreased(Id, item.Id, cmd.Quantity, item.UnitPrice));
    }

    public void Handle(Command.RemoveCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;
        RaiseEvent(new DomainEvent.CartItemRemoved(cmd.Id, cmd.ItemId, item.UnitPrice, item.Quantity));
    }

    public void Handle(Command.AddPaymentMethod cmd)
    {
        // TODO - Should cmd.Amount be subtracted from AmountDue?
        if (cmd.Amount > AmountDue) return;
        RaiseEvent(new DomainEvent.PaymentMethodAdded(cmd.Id, Guid.NewGuid(), cmd.Amount, cmd.Option));
    }

    public void Handle(Command.AddShippingAddress cmd)
    {
        if (ShippingAddress == cmd.Address) return;
        RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.Id, cmd.Address));
    }

    public void Handle(Command.AddBillingAddress cmd)
    {
        if (BillingAddress == cmd.Address) return;
        RaiseEvent(new DomainEvent.BillingAddressAdded(cmd.Id, cmd.Address));
    }

    public void Handle(Command.CheckOutCart cmd)
    {
        if (_items.Any() is false || AmountDue > 0) return;
        RaiseEvent(new DomainEvent.CartCheckedOut(cmd.Id));
    }

    public void Handle(Command.DiscardCart cmd)
        => RaiseEvent(new DomainEvent.CartDiscarded(cmd.Id));

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
        => _items
            .First(item => item.Id == @event.ItemId)
            .SetQuantity(@event.NewQuantity);

    private void When(DomainEvent.CartItemDecreased @event)
        => _items
            .First(item => item.Id == @event.ItemId)
            .SetQuantity(@event.NewQuantity);

    private void When(DomainEvent.CartItemRemoved @event)
        => _items.First(item => item.Id == @event.ItemId).Delete();

    private void When(DomainEvent.CartItemAdded @event)
        => _items.Add(new(@event.ItemId, @event.CatalogId, @event.Product, @event.Quantity, @event.UnitPrice));

    private void When(DomainEvent.PaymentMethodAdded @event)
        => _paymentMethods.Add(new(@event.MethodId, @event.Amount, @event.Option switch
        {
            Dto.CreditCard creditCard => (CreditCard)creditCard,
            Dto.DebitCard debitCard => (DebitCard)debitCard,
            Dto.PayPal payPal => (PayPal)payPal,
            _ => default
        }));

    private void When(DomainEvent.BillingAddressAdded @event)
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
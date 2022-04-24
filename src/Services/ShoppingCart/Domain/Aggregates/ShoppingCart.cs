using Domain.Abstractions.Aggregates;
using Domain.Entities.Customers;
using Domain.Entities.ShoppingCartItems;
using Domain.Enumerations;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.ShoppingCarts;

namespace Domain.Aggregates;

public class ShoppingCart : AggregateRoot<Guid, ShoppingCartValidator>
{
    private readonly List<ShoppingCartItem> _items = new();
    private readonly List<IPaymentMethod> _paymentMethods = new();

    public ShoppingCartStatus Status { get; private set; }
    public Customer Customer { get; private set; }

    public decimal Total
        => Items.Sum(item => item.UnitPrice * item.Quantity);

    public IEnumerable<ShoppingCartItem> Items
        => _items;

    public IEnumerable<IPaymentMethod> PaymentMethods
        => _paymentMethods;

    public void Handle(Command.CreateCart cmd)
        => RaiseEvent(new DomainEvent.CartCreated(Guid.NewGuid(), cmd.CustomerId, ShoppingCartStatus.Confirmed.ToString()));

    public void Handle(Command.AddCartItem cmd)
    {
        var item = _items.SingleOrDefault(item => item.ProductId == cmd.Item.ProductId);

        RaiseEvent(item is null
            ? new DomainEvent.CartItemAdded(cmd.CartId, Guid.NewGuid(), cmd.Item)
            : new DomainEvent.CartItemIncreased(cmd.CartId, item.Id, item.UnitPrice));
    }

    public void Handle(Command.IncreaseShoppingCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemIncreased(cmd.CartId, cmd.ItemId, item.UnitPrice));
    }

    public void Handle(Command.DecreaseShoppingCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemDecreased(cmd.CartId, cmd.ItemId, item.UnitPrice));
    }

    public void Handle(Command.RemoveCartItem cmd)
    {
        if (_items.SingleOrDefault(cartItem => cartItem.Id == cmd.ItemId) is {IsDeleted: false} item)
            RaiseEvent(new DomainEvent.CartItemRemoved(cmd.CartId, cmd.ItemId, item.UnitPrice, item.Quantity));
    }

    public void Handle(Command.AddCreditCard cmd)
        => RaiseEvent(new DomainEvent.CreditCardAdded(cmd.CartId, cmd.CreditCard));

    public void Handle(Command.AddPayPal cmd)
        => RaiseEvent(new DomainEvent.PayPalAdded(cmd.CartId, cmd.PayPal));

    public void Handle(Command.AddShippingAddress cmd)
        => RaiseEvent(new DomainEvent.ShippingAddressAdded(cmd.CartId, cmd.Address));

    public void Handle(Command.ChangeBillingAddress cmd)
        => RaiseEvent(new DomainEvent.BillingAddressChanged(cmd.CartId, cmd.Address));

    public void Handle(Command.CheckOutCart cmd)
        => RaiseEvent(new DomainEvent.CartCheckedOut(cmd.CartId));

    public void Handle(Command.DiscardCart cmd)
        => RaiseEvent(new DomainEvent.CartDiscarded(cmd.CartId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CartCreated @event)
    {
        Id = @event.CartId;
        Customer = new(@event.CustomerId);
        Status = ShoppingCartStatus.FromName(@event.Status);
    }

    private void When(DomainEvent.CartCheckedOut _)
        => Status = ShoppingCartStatus.CheckedOut;

    private void When(DomainEvent.CartDiscarded _)
        => IsDeleted = true;

    private void When(DomainEvent.CartItemIncreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Increase();

    private void When(DomainEvent.CartItemDecreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Decrease();

    private void When(DomainEvent.CartItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);

    private void When(DomainEvent.CartItemAdded @event)
        => _items.Add(new(
            @event.ItemId,
            @event.Item.ProductId,
            @event.Item.ProductName,
            @event.Item.UnitPrice,
            @event.Item.Quantity,
            @event.Item.PictureUrl,
            @event.Item.Sku));

    private void When(DomainEvent.CreditCardAdded @event)
        => _paymentMethods.Add(
            new CreditCardPaymentMethod(
                @event.CreditCard.Amount,
                @event.CreditCard.Expiration,
                @event.CreditCard.HolderName,
                @event.CreditCard.Number,
                @event.CreditCard.SecurityNumber));

    private void When(DomainEvent.PayPalAdded @event)
        => _paymentMethods.Add(
            new PayPalPaymentMethod(
                @event.PayPal.Amount,
                @event.PayPal.UserName,
                @event.PayPal.Password));

    private void When(DomainEvent.ShippingAddressAdded @event)
        => Customer.SetShippingAddress(new()
        {
            City = @event.Address.City,
            Country = @event.Address.Country,
            Number = @event.Address.Number,
            State = @event.Address.State,
            Street = @event.Address.Street,
            ZipCode = @event.Address.ZipCode
        });

    private void When(DomainEvent.BillingAddressChanged @event)
        => Customer.SetBillingAddress(new()
        {
            City = @event.Address.City,
            Country = @event.Address.Country,
            Number = @event.Address.Number,
            State = @event.Address.State,
            Street = @event.Address.Street,
            ZipCode = @event.Address.ZipCode
        });
}
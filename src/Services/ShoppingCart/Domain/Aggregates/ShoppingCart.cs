using Domain.Abstractions.Aggregates;
using Domain.Entities.Customers;
using Domain.Entities.ShoppingCartItems;
using Domain.Enumerations;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.ShoppingCart;

namespace Domain.Aggregates;

public class ShoppingCart : AggregateRoot<Guid, ShoppingCartValidator>
{
    private readonly List<ShoppingCartItem> _items = new();
    private readonly List<IPaymentMethod> _paymentMethods = new();

    public ShoppingCartStatus Status { get; private set; } = ShoppingCartStatus.Confirmed;
    public Customer Customer { get; private set; }

    public decimal Total
        => Items.Sum(item => item.UnitPrice * item.Quantity);

    public IEnumerable<ShoppingCartItem> Items
        => _items;

    public IEnumerable<IPaymentMethod> PaymentMethods
        => _paymentMethods;

    public void Handle(Commands.CreateCart cmd)
        => RaiseEvent(new DomainEvents.CartCreated(Guid.NewGuid(), cmd.CustomerId));

    public void Handle(Commands.AddCartItem cmd)
    {
        var item = _items.SingleOrDefault(item => item.ProductId == cmd.Item.ProductId);

        RaiseEvent(item is null
            ? new DomainEvents.CartItemAdded(cmd.CartId, Guid.NewGuid(), cmd.Item.ProductId, cmd.Item.ProductName, cmd.Item.UnitPrice, cmd.Item.Quantity, cmd.Item.PictureUrl)
            : new DomainEvents.CartItemIncreased(cmd.CartId, item.Id));
    }

    public void Handle(Commands.IncreaseShoppingCartItem cmd)
    {
        if (_items.Exists(item => item.Id == cmd.ItemId))
            RaiseEvent(new DomainEvents.CartItemIncreased(cmd.CartId, cmd.ItemId));
    }

    public void Handle(Commands.DecreaseShoppingCartItem cmd)
    {
        if (_items.Exists(item => item.Id == cmd.ItemId))
            RaiseEvent(new DomainEvents.CartItemDecreased(cmd.CartId, cmd.ItemId));
    }

    public void Handle(Commands.RemoveCartItem cmd)
    {
        if (_items.Exists(item => item.Id == cmd.ItemId))
            RaiseEvent(new DomainEvents.CartItemRemoved(cmd.CartId, cmd.ItemId));
    }

    public void Handle(Commands.AddCreditCard cmd)
        => RaiseEvent(new DomainEvents.CreditCardAdded(cmd.CartId, cmd.CreditCard));

    public void Handle(Commands.AddPayPal cmd)
        => RaiseEvent(new DomainEvents.PayPalAdded(cmd.CartId, cmd.PayPal));

    public void Handle(Commands.AddShippingAddress cmd)
        => RaiseEvent(new DomainEvents.ShippingAddressAdded(cmd.CartId, cmd.Address));

    public void Handle(Commands.ChangeBillingAddress cmd)
        => RaiseEvent(new DomainEvents.BillingAddressChanged(cmd.CartId, cmd.Address));

    public void Handle(Commands.CheckOutCart cmd)
        => RaiseEvent(new DomainEvents.CartCheckedOut(cmd.CartId));

    public void Handle(Commands.DiscardCart cmd)
        => RaiseEvent(new DomainEvents.CartDiscarded(cmd.CartId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.CartCreated @event)
    {
        Id = @event.CartId;
        Customer = new(@event.CustomerId);
    }

    private void When(DomainEvents.CartCheckedOut _)
        => Status = ShoppingCartStatus.CheckedOut;

    private void When(DomainEvents.CartDiscarded _)
        => IsDeleted = true;

    private void When(DomainEvents.CartItemIncreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Increase();

    private void When(DomainEvents.CartItemDecreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Decrease();

    private void When(DomainEvents.CartItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);

    private void When(DomainEvents.CartItemAdded @event)
        => _items.Add(new(
            @event.ItemId,
            @event.ProductId,
            @event.ProductName,
            @event.UnitPrice,
            @event.Quantity,
            @event.PictureUrl));

    private void When(DomainEvents.CreditCardAdded @event)
        => _paymentMethods.Add(
            new CreditCardPaymentMethod(
                @event.CreditCard.Amount,
                @event.CreditCard.Expiration,
                @event.CreditCard.HolderName,
                @event.CreditCard.Number,
                @event.CreditCard.SecurityNumber));

    private void When(DomainEvents.PayPalAdded @event)
        => _paymentMethods.Add(
            new PayPalPaymentMethod(
                @event.PayPal.Amount,
                @event.PayPal.UserName,
                @event.PayPal.Password));

    private void When(DomainEvents.ShippingAddressAdded @event)
        => Customer.SetShippingAddress(new()
        {
            City = @event.Address.City,
            Country = @event.Address.Country,
            Number = @event.Address.Number,
            State = @event.Address.State,
            Street = @event.Address.Street,
            ZipCode = @event.Address.ZipCode
        });

    private void When(DomainEvents.BillingAddressChanged @event)
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
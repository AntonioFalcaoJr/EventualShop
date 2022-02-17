using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.ShoppingCart;

namespace Domain.Aggregates;

public class Cart : AggregateRoot<Guid>
{
    private readonly List<CartItem> _items = new();
    private readonly List<IPaymentMethod> _paymentMethods = new();

    public Guid CustomerId { get; private set; }
    public CartStatus Status { get; private set; } = CartStatus.Confirmed;
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    private bool ShippingAndBillingAddressesAreSame { get; set; } = true;

    public decimal Total
        => Items.Sum(item => item.UnitPrice * item.Quantity);

    public IEnumerable<CartItem> Items
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

    public void Handle(Commands.IncreaseCartItemQuantity cmd)
        => RaiseEvent(new DomainEvents.CartItemIncreased(cmd.CartId, cmd.ItemId));

    public void Handle(Commands.DecreaseCartItemQuantity cmd)
        => RaiseEvent(new DomainEvents.CartItemDecreased(cmd.CartId, cmd.ItemId));

    public void Handle(Commands.RemoveCartItem cmd)
        => RaiseEvent(new DomainEvents.CartItemRemoved(cmd.CartId, cmd.ItemId));

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
        => (Id, CustomerId) = @event;

    private void When(DomainEvents.CartCheckedOut _)
        => Status = CartStatus.CheckedOut;

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
    {
        ShippingAddress = new()
        {
            City = @event.Address.City,
            Country = @event.Address.Country,
            Number = @event.Address.Number,
            State = @event.Address.State,
            Street = @event.Address.Street,
            ZipCode = @event.Address.ZipCode
        };

        if (ShippingAndBillingAddressesAreSame)
            BillingAddress = ShippingAddress;
    }

    private void When(DomainEvents.BillingAddressChanged @event)
    {
        BillingAddress = new()
        {
            City = @event.Address.City,
            Country = @event.Address.Country,
            Number = @event.Address.Number,
            State = @event.Address.State,
            Street = @event.Address.Street,
            ZipCode = @event.Address.ZipCode
        };

        ShippingAndBillingAddressesAreSame = false;
    }

    protected sealed override bool Validate()
        => OnValidate<CartValidator, Cart>();
}
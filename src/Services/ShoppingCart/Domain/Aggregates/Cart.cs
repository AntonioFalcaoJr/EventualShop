using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CartItems;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Messages.Abstractions.Events;
using Messages.Services.ShoppingCarts;

namespace Domain.Aggregates;

public class Cart : AggregateRoot<Guid>
{
    private readonly List<CartItem> _items = new();
    private readonly List<IPaymentMethod> _paymentMethods = new();
    public Guid UserId { get; private set; }
    public bool IsCheckedOut { get; private set; }
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
        => RaiseEvent(_items.Exists(item => item.ProductId == cmd.Item.ProductId)
            ? new DomainEvents.CartItemQuantityIncreased(cmd.CartId, cmd.Item.ProductId, cmd.Item.Quantity)
            : new DomainEvents.CartItemAdded(cmd.CartId, cmd.Item));

    public void Handle(Commands.UpdateCartItemQuantity cmd)
    {
        if (_items.Exists(item => item.ProductId == cmd.ProductId))
            RaiseEvent(cmd.Quantity > 0
                ? new DomainEvents.CartItemQuantityUpdated(cmd.CartId, cmd.ProductId, cmd.Quantity)
                : new DomainEvents.CartItemRemoved(cmd.CartId, cmd.ProductId));
    }

    public void Handle(Commands.RemoveCartItem cmd)
    {
        if (_items.Exists(item => item.ProductId == cmd.ProductId))
            RaiseEvent(new DomainEvents.CartItemRemoved(cmd.CartId, cmd.ProductId));
    }

    public void Handle(Commands.AddCreditCard cmd)
        => RaiseEvent(new DomainEvents.CreditCardAdded(cmd.CartId, cmd.CreditCard));

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
        => (Id, UserId) = @event;

    private void When(DomainEvents.CartCheckedOut _)
        => IsCheckedOut = true;

    private void When(DomainEvents.CartDiscarded _)
        => IsDeleted = true;

    private void When(DomainEvents.CartItemQuantityIncreased @event)
        => _items.Single(item => item.ProductId == @event.ProductId).IncreaseQuantity(@event.Quantity);

    private void When(DomainEvents.CartItemQuantityUpdated @event)
        => _items.Single(item => item.ProductId == @event.ProductId).UpdateQuantity(@event.Quantity);

    private void When(DomainEvents.CartItemRemoved @event)
        => _items.RemoveAll(item => item.ProductId == @event.ProductId);

    private void When(DomainEvents.CartItemAdded @event)
        => _items.Add(new CartItem
        {
            ProductId = @event.Item.ProductId,
            ProductName = @event.Item.ProductName,
            UnitPrice = @event.Item.UnitPrice,
            Quantity = @event.Item.Quantity,
            PictureUrl = @event.Item.PictureUrl
        });

    private void When(DomainEvents.CreditCardAdded @event)
        => _paymentMethods.Add(new CreditCard
        {
            Amount = @event.CreditCard.Amount,
            Expiration = @event.CreditCard.Expiration,
            HolderName = @event.CreditCard.HolderName,
            Number = @event.CreditCard.Number,
            SecurityNumber = @event.CreditCard.SecurityNumber
        });

    private void When(DomainEvents.ShippingAddressAdded @event)
    {
        ShippingAddress = new Address
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
        BillingAddress = new Address
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
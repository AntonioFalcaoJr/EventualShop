using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;
using Messages.Abstractions.Events;
using Messages.Services.ShoppingCarts;

namespace Domain.Aggregates;

public class Cart : AggregateRoot<Guid>
{
    private readonly List<CartItem> _items = new();
    public Guid UserId { get; private set; }
    public bool IsCheckedOut { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public CreditCard CreditCard { get; private set; }
    private bool ShippingAndBillingAddressesAreSame { get; set; } = true;

    public decimal Total
        => Items.Sum(item
            => item.UnitPrice * item.Quantity);

    public IEnumerable<CartItem> Items
        => _items;

    public void Handle(Commands.CreateCart cmd)
        => RaiseEvent(new DomainEvents.CartCreated(Guid.NewGuid(), cmd.CustomerId));

    public void Handle(Commands.AddCartItem cmd)
        => RaiseEvent(new DomainEvents.CartItemAdded(cmd.CartId, cmd.Product, cmd.Quantity));

    public void Handle(Commands.AddCreditCard cmd)
        => RaiseEvent(new DomainEvents.CreditCardAdded(cmd.CartId, cmd.Expiration, cmd.HolderName, cmd.Number, cmd.SecurityNumber));

    public void Handle(Commands.AddShippingAddress cmd)
        => RaiseEvent(new DomainEvents.ShippingAddressAdded(cmd.CartId, cmd.City, cmd.Country, cmd.Number, cmd.State, cmd.Street, cmd.ZipCode));

    public void Handle(Commands.ChangeBillingAddress cmd)
        => RaiseEvent(new DomainEvents.BillingAddressChanged(cmd.CartId, cmd.City, cmd.Country, cmd.Number, cmd.State, cmd.Street, cmd.ZipCode));

    public void Handle(Commands.RemoveCartItem cmd)
        => RaiseEvent(new DomainEvents.CartItemRemoved(Id, cmd.ProductId));

    public void Handle(Commands.CheckOutCart cmd)
        => RaiseEvent(new DomainEvents.CartCheckedOut(cmd.CartId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.CartCreated @event)
        => (Id, UserId) = @event;

    private void When(DomainEvents.CartCheckedOut _)
        => IsCheckedOut = true;

    private void When(DomainEvents.CartItemAdded @event)
    {
        if (_items.Exists(item => item.CatalogItemId == @event.Product.Id))
            IncreaseItemQuantity(@event);
        else AddNewItem(@event);
    }

    private void When(DomainEvents.CartItemRemoved @event)
        => _items.RemoveAll(item
            => item.CatalogItemId == @event.CatalogItemId);

    private void AddNewItem(DomainEvents.CartItemAdded @event)
        => _items.Add(
            new CartItem(
                @event.Product.Id,
                @event.Product.Name,
                @event.Product.UnitPrice,
                @event.Quantity));

    private void When(DomainEvents.CreditCardAdded @event)
        => CreditCard =
            new CreditCard
            {
                Expiration = @event.Expiration,
                HolderName = @event.HolderName,
                Number = @event.Number,
                SecurityNumber = @event.SecurityNumber
            };

    private void When(DomainEvents.ShippingAddressAdded @event)
    {
        ShippingAddress = new Address
        {
            City = @event.City,
            Country = @event.Country,
            Number = @event.Number,
            State = @event.State,
            Street = @event.Street,
            ZipCode = @event.ZipCode
        };

        if (ShippingAndBillingAddressesAreSame)
            BillingAddress = ShippingAddress;
    }

    private void When(DomainEvents.BillingAddressChanged @event)
    {
        BillingAddress = new Address
        {
            City = @event.City,
            Country = @event.Country,
            Number = @event.Number,
            State = @event.State,
            Street = @event.Street,
            ZipCode = @event.ZipCode
        };

        ShippingAndBillingAddressesAreSame = false;
    }

    private void IncreaseItemQuantity(DomainEvents.CartItemAdded @event)
        => _items
            .Single(item => item.CatalogItemId == @event.Product.Id)
            .IncreaseQuantity(@event.Quantity);

    protected sealed override bool Validate()
        => OnValidate<CartValidator, Cart>();
}
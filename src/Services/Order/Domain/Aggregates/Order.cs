using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.OrderItems;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;
using Messages.Abstractions.Events;
using Messages.Services.Orders;

namespace Domain.Aggregates;

public class Order : AggregateRoot<Guid>
{
    private readonly List<OrderItem> _items = new();
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public CreditCard CreditCard { get; private set; }
    public Guid UserId { get; private set; }

    public decimal Total
        => Items.Sum(item
            => item.Price * item.Quantity);

    public IEnumerable<OrderItem> Items
        => _items;

    public void Handle(Commands.PlaceOrder command)
        => RaiseEvent(
            new Events.OrderPlaced(
                Guid.NewGuid(),
                command.CustomerId,
                command.Items,
                command.BillingAddress,
                command.CreditCard,
                command.ShippingAddress));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(Events.OrderPlaced @event)
    {
        Id = @event.OrderId;
        UserId = @event.CustomerId;

        BillingAddress = new()
        {
            City = @event.BillingAddress.City,
            Country = @event.BillingAddress.Country,
            Number = @event.BillingAddress.Number,
            State = @event.BillingAddress.State,
            Street = @event.BillingAddress.Street,
            ZipCode = @event.BillingAddress.ZipCode
        };

        CreditCard = new()
        {
            Expiration = @event.CreditCard.Expiration,
            Number = @event.CreditCard.Number,
            HolderName = @event.CreditCard.HolderName,
            SecurityNumber = @event.CreditCard.SecurityNumber
        };

        ShippingAddress = new()
        {
            City = @event.ShippingAddress.City,
            Country = @event.ShippingAddress.Country,
            Number = @event.ShippingAddress.Number,
            State = @event.ShippingAddress.State,
            Street = @event.ShippingAddress.Street,
            ZipCode = @event.ShippingAddress.ZipCode
        };

        _items.AddRange(@event.Items.Select(item
            => new OrderItem(
                item.CatalogItemId,
                item.ProductName,
                "SKU",
                "CATEGORY",
                "BRAND",
                item.UnitPrice,
                item.Quantity,
                item.PictureUrl)));
    }

    protected sealed override bool Validate()
        => OnValidate<OrderValidator, Order>();
}
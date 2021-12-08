using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.OrderItems;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;
using Domain.Entities.PaymentMethods.DebitCards;
using Domain.Entities.PaymentMethods.PayPal;
using Domain.ValueObjects.Addresses;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.Order;

namespace Domain.Aggregates;

public class Order : AggregateRoot<Guid>
{
    private readonly List<OrderItem> _items = new();
    private readonly List<IPaymentMethod> _paymentMethods = new();
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public Guid UserId { get; private set; }

    public decimal Total
        => Items.Sum(item
            => item.Price * item.Quantity);

    public IEnumerable<IPaymentMethod> PaymentMethods
        => _paymentMethods;

    public IEnumerable<OrderItem> Items
        => _items;

    public void Handle(Commands.PlaceOrder cmd)
        => RaiseEvent(new DomainEvents.OrderPlaced(
            Guid.NewGuid(),
            cmd.CustomerId,
            cmd.Total,
            cmd.Items,
            cmd.BillingAddress,
            cmd.ShippingAddress,
            cmd.PaymentMethods));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.OrderPlaced @event)
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
                item.ProductId,
                item.ProductName,
                "SKU",
                "CATEGORY",
                "BRAND",
                item.UnitPrice,
                item.Quantity,
                item.PictureUrl)));

        _paymentMethods.AddRange(@event.PaymentMethods.Select<Models.IPaymentMethod, IPaymentMethod>(method
            => method switch
            {
                Models.CreditCard creditCard
                    => new CreditCardPaymentMethod
                    {
                        Amount = creditCard.Amount,
                        Expiration = creditCard.Expiration,
                        Number = creditCard.Number,
                        HolderName = creditCard.HolderName,
                        SecurityNumber = creditCard.SecurityNumber
                    },
                Models.DebitCard debitCard
                    => new DebitCardPaymentMethod
                    {
                        Amount = debitCard.Amount,
                        Expiration = debitCard.Expiration,
                        Number = debitCard.Number,
                        HolderName = debitCard.HolderName,
                        SecurityNumber = debitCard.SecurityNumber
                    },
                Models.PayPal payPal
                    => new PayPalPaymentMethod
                    {
                        Amount = payPal.Amount,
                        Password = payPal.Password,
                        UserName = payPal.UserName
                    },
                _ => default
            }));
    }

    protected sealed override bool Validate()
        => OnValidate<OrderValidator, Order>();
}
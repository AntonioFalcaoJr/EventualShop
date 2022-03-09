using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.Customers;
using Domain.Entities.OrderItems;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;
using Domain.Entities.PaymentMethods.DebitCards;
using Domain.Entities.PaymentMethods.PayPal;
using Domain.Enumerations;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.Order;

namespace Domain.Aggregates;

public class Order : AggregateRoot<Guid>
{
    private readonly List<OrderItem> _items = new();
    private readonly List<IPaymentMethod> _paymentMethods = new();
    public OrderStatus Status { get; private set; } = OrderStatus.PendingPayment;
    public Customer Customer { get; private set; }

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
            cmd.Customer,
            cmd.Items,
            cmd.Total,
            cmd.PaymentMethods));

    public void Handle(Commands.ConfirmOrder cmd)
        => RaiseEvent(new DomainEvents.OrderConfirmed(cmd.OrderId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.OrderPlaced @event)
    {
        Id = @event.OrderId;
        Customer = new(
            @event.Customer.Id,
            new()
            {
                City = @event.Customer.BillingAddress.City,
                Country = @event.Customer.BillingAddress.Country,
                Number = @event.Customer.BillingAddress.Number,
                State = @event.Customer.BillingAddress.State,
                Street = @event.Customer.BillingAddress.Street,
                ZipCode = @event.Customer.BillingAddress.ZipCode
            },
            new()
            {
                City = @event.Customer.ShippingAddress.City,
                Country = @event.Customer.ShippingAddress.Country,
                Number = @event.Customer.ShippingAddress.Number,
                State = @event.Customer.ShippingAddress.State,
                Street = @event.Customer.ShippingAddress.Street,
                ZipCode = @event.Customer.ShippingAddress.ZipCode
            });
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

    private void When(DomainEvents.OrderConfirmed _)
        => Status = OrderStatus.Confirmed;

    protected sealed override bool Validate()
        => OnValidate<OrderValidator, Order>();
}
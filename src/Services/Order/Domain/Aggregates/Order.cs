﻿using Domain.Abstractions.Aggregates;
using Domain.Entities.Customers;
using Domain.Entities.OrderItems;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;
using Domain.Entities.PaymentMethods.DebitCards;
using Domain.Entities.PaymentMethods.PayPal;
using Domain.Enumerations;
using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Order;

namespace Domain.Aggregates;

public class Order : AggregateRoot<Guid, OrderValidator>
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

    public void Handle(Command.PlaceOrder cmd)
        => RaiseEvent(new DomainEvent.OrderPlaced(
            Guid.NewGuid(),
            cmd.Customer,
            cmd.Items,
            cmd.Total,
            cmd.PaymentMethods));

    public void Handle(Command.ConfirmOrder cmd)
        => RaiseEvent(new DomainEvent.OrderConfirmed(cmd.OrderId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.OrderPlaced @event)
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
                item.Product.Id,
                item.Product.Name,
                item.Product.Sku,
                "CATEGORY",
                "BRAND",
                item.Product.UnitPrice,
                item.Quantity,
                item.Product.PictureUrl)));

        _paymentMethods.AddRange(@event.PaymentMethods.Select<Dto.IPaymentMethod, IPaymentMethod>(method
            => method switch
            {
                Dto.CreditCard creditCard
                    => new CreditCardPaymentMethod
                    {
                        Amount = creditCard.Amount,
                        Expiration = creditCard.Expiration,
                        Number = creditCard.Number,
                        HolderName = creditCard.HolderName,
                        SecurityNumber = creditCard.SecurityNumber
                    },
                Dto.DebitCard debitCard
                    => new DebitCardPaymentMethod
                    {
                        Amount = debitCard.Amount,
                        Expiration = debitCard.Expiration,
                        Number = debitCard.Number,
                        HolderName = debitCard.HolderName,
                        SecurityNumber = debitCard.SecurityNumber
                    },
                Dto.PayPal payPal
                    => new PayPalPaymentMethod
                    {
                        Amount = payPal.Amount,
                        Password = payPal.Password,
                        UserName = payPal.UserName
                    },
                _ => default
            }));
    }

    private void When(DomainEvent.OrderConfirmed _)
        => Status = OrderStatus.Confirmed;
}
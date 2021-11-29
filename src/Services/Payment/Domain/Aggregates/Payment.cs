using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;
using Domain.Entities.PaymentMethods.DebitCards;
using Domain.Entities.PaymentMethods.PayPal;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Messages;
using Messages.Abstractions.Events;
using Messages.Services.Payments;

namespace Domain.Aggregates;

public class Payment : AggregateRoot<Guid>
{
    private readonly List<IPaymentMethod> _paymentMethods = new();

    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public Address BillingAddress { get; private set; }

    public decimal AmountDue
        => _paymentMethods
            .Where(method => method.Authorized is false)
            .Sum(method => method.Amount);

    public IEnumerable<IPaymentMethod> PaymentMethods
        => _paymentMethods;

    public void Handle(Commands.RequestPayment cmd)
        => RaiseEvent(new DomainEvents.PaymentRequested(Guid.NewGuid(), cmd.OrderId, cmd.AmountDue, cmd.BillingAddress, cmd.PaymentMethods));

    public void Handle(Commands.ProceedWithPayment cmd)
        => RaiseEvent(AmountDue > 0
            ? new DomainEvents.PaymentNotCompleted(cmd.PaymentId, cmd.OrderId)
            : new DomainEvents.PaymentCompleted(cmd.PaymentId, cmd.OrderId));

    public void Handle(Commands.CancelPayment cmd)
        => RaiseEvent(new DomainEvents.PaymentCanceled(cmd.PaymentId, cmd.OrderId));

    public void Handle(Commands.UpdatePaymentMethod cmd)
        => RaiseEvent(cmd.Authorized
            ? new DomainEvents.PaymentMethodAuthorized(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId)
            : new DomainEvents.PaymentMethodDenied(cmd.PaymentId, cmd.PaymentMethodId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.PaymentRequested @event)
    {
        Id = @event.PaymentId;
        OrderId = @event.OrderId;
        Amount = @event.Amount;

        BillingAddress = new()
        {
            City = @event.BillingAddress.City,
            Country = @event.BillingAddress.Country,
            Number = @event.BillingAddress.Number,
            State = @event.BillingAddress.State,
            Street = @event.BillingAddress.Street,
            ZipCode = @event.BillingAddress.ZipCode
        };

        _paymentMethods.AddRange(@event.PaymentMethods.Select<Models.IPaymentMethod, IPaymentMethod>(method
            => method switch
            {
                Models.CreditCard creditCard => new CreditCardPaymentMethod
                {
                    Amount = creditCard.Amount,
                    Expiration = creditCard.Expiration,
                    Number = creditCard.Number,
                    HolderName = creditCard.HolderName,
                    SecurityNumber = creditCard.SecurityNumber
                },
                Models.DebitCard debitCard => new DebitCardPaymentMethod
                {
                    Amount = debitCard.Amount,
                    Expiration = debitCard.Expiration,
                    Number = debitCard.Number,
                    HolderName = debitCard.HolderName,
                    SecurityNumber = debitCard.SecurityNumber
                },
                Models.PayPal payPal => new PayPalPaymentMethod
                {
                    Amount = payPal.Amount,
                    Password = payPal.Password,
                    UserName = payPal.UserName
                }
            }));

        Status = PaymentStatus.Pending;
    }

    private void When(DomainEvents.PaymentMethodAuthorized @event)
        => _paymentMethods
            .First(method => method.Id == @event.PaymentMethodId)
            .Authorize();

    private void When(DomainEvents.PaymentCompleted _)
        => Status = PaymentStatus.Complete;

    private void When(DomainEvents.PaymentNotCompleted _)
        => Status = PaymentStatus.Insufficient;

    protected override bool Validate()
        => true;
}
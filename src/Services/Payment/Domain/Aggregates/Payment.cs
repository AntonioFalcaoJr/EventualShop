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
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.Payment;

namespace Domain.Aggregates;

public class Payment : AggregateRoot<Guid>
{
    private readonly List<IPaymentMethod> _methods = new();

    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; } = PaymentStatus.Ready;
    public Address BillingAddress { get; private set; }

    public decimal AmountDue
        => _methods
            .Where(method => method.Status is not PaymentMethodStatus.Authorized)
            .Sum(method => method.Amount);

    public IEnumerable<IPaymentMethod> Methods
        => _methods;

    public void Handle(Commands.RequestPayment cmd)
        => RaiseEvent(new DomainEvents.PaymentRequested(Guid.NewGuid(), cmd.OrderId, cmd.AmountDue, cmd.BillingAddress, cmd.PaymentMethods));

    public void Handle(Commands.ProceedWithPayment cmd)
        => RaiseEvent(AmountDue is 0
            ? new DomainEvents.PaymentCompleted(cmd.PaymentId, cmd.OrderId)
            : new DomainEvents.PaymentNotCompleted(cmd.PaymentId, cmd.OrderId));

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

        _methods.AddRange(@event.PaymentMethods
            .Select<Models.IPaymentMethod, IPaymentMethod>(method
                => method switch
                {
                    Models.CreditCard creditCard
                        => new CreditCardPaymentMethod(
                            creditCard.Id,
                            creditCard.Amount,
                            creditCard.Expiration,
                            creditCard.Number,
                            creditCard.HolderName,
                            creditCard.SecurityNumber),
                    Models.DebitCard debitCard
                        => new DebitCardPaymentMethod(
                            debitCard.Id,
                            debitCard.Amount,
                            debitCard.Expiration,
                            debitCard.Number,
                            debitCard.HolderName,
                            debitCard.SecurityNumber),
                    Models.PayPal payPal
                        => new PayPalPaymentMethod(
                            payPal.Id,
                            payPal.Amount,
                            payPal.Password,
                            payPal.UserName),
                    _ => default
                }));

        Status = PaymentStatus.Ready;
    }

    private void When(DomainEvents.PaymentMethodAuthorized @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Authorize();

    private void When(DomainEvents.PaymentMethodDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Deny();

    private void When(DomainEvents.PaymentCompleted _)
        => Status = PaymentStatus.Completed;

    private void When(DomainEvents.PaymentNotCompleted _)
        => Status = PaymentStatus.Pending;

    protected override bool Validate()
        => true;
}
using Domain.Abstractions.Aggregates;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Payment;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;
using Domain.ValueObjects.PaymentOptions.PayPals;

namespace Domain.Aggregates;

public class Payment : AggregateRoot<Guid, PaymentValidator>
{
    private readonly List<PaymentMethod> _paymentMethods = new();

    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public Address BillingAddress { get; private set; }

    public decimal AmountDue
        => _paymentMethods
            .Where(method => method.Status is not PaymentMethodStatus.Authorized)
            .Sum(method => method.Amount);

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods;

    public void Handle(Command.RequestPayment cmd)
        => RaiseEvent(new DomainEvent.PaymentRequested(Guid.NewGuid(), cmd.OrderId, cmd.AmountDue, cmd.BillingAddress, cmd.PaymentMethods, PaymentStatus.Ready.ToString()));

    public void Handle(Command.ProceedWithPayment cmd)
        => RaiseEvent(AmountDue is 0
            ? new DomainEvent.PaymentCompleted(cmd.PaymentId, cmd.OrderId)
            : new DomainEvent.PaymentNotCompleted(cmd.PaymentId, cmd.OrderId));

    public void Handle(Command.CancelPayment cmd)
        => RaiseEvent(new DomainEvent.PaymentCanceled(cmd.PaymentId, cmd.OrderId));

    public void Handle(Command.UpdatePaymentMethod cmd)
        => RaiseEvent(cmd.Authorized
            ? new DomainEvent.PaymentMethodAuthorized(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId)
            : new DomainEvent.PaymentMethodDenied(cmd.PaymentId, cmd.PaymentMethodId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.PaymentRequested @event)
    {
        Id = @event.PaymentId;
        OrderId = @event.OrderId;
        Amount = @event.Amount;
        BillingAddress = @event.BillingAddress;
        _paymentMethods.AddRange(@event.PaymentMethods.Select<Dto.PaymentMethod, PaymentMethod>(method
            => method.Option switch
            {
                Dto.CreditCard creditCard => new(method.Id, method.Amount, (CreditCard) creditCard),
                Dto.DebitCard debitCard => new(method.Id, method.Amount, (DebitCard) debitCard),
                Dto.PayPal payPal => new(method.Id, method.Amount, (PayPal) payPal),
                _ => default
            }));

        Status = PaymentStatus.FromName(@event.Status);
    }

    private void When(DomainEvent.PaymentMethodAuthorized @event)
        => _paymentMethods.Single(method => method.Id == @event.PaymentMethodId).Authorize();

    private void When(DomainEvent.PaymentMethodDenied @event)
        => _paymentMethods.Single(method => method.Id == @event.PaymentMethodId).Deny();

    private void When(DomainEvent.PaymentCompleted _)
        => Status = PaymentStatus.Completed;

    private void When(DomainEvent.PaymentNotCompleted _)
        => Status = PaymentStatus.Pending;
}
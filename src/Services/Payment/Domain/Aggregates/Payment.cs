using Domain.Abstractions.Aggregates;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Contracts.Abstractions.Messages;
using Contracts.Services.Payment;

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
        => RaiseEvent(new DomainEvent.PaymentRequested(Guid.NewGuid(), cmd.OrderId, cmd.AmountDue, cmd.BillingAddress, cmd.PaymentMethods, PaymentStatus.Ready));

    public void Handle(Command.ProceedWithPayment cmd)
        => RaiseEvent(AmountDue is 0
            ? new DomainEvent.PaymentCompleted(cmd.PaymentId, cmd.OrderId)
            : new DomainEvent.PaymentNotCompleted(cmd.PaymentId, cmd.OrderId));

    public void Handle(Command.CancelPayment cmd)
        => RaiseEvent(new DomainEvent.PaymentCanceled(cmd.PaymentId, cmd.OrderId));

    public void Handle(Command.AuthorizePaymentMethod cmd)
    {
        if (_paymentMethods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodAuthorized(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.DenyPaymentMethod cmd)
    {
        if (_paymentMethods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.CancelPaymentMethod cmd)
    {
        if (_paymentMethods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodCanceled(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.DenyPaymentMethodCancellation cmd)
    {
        if (_paymentMethods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodCancellationDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.RefundPaymentMethod cmd)
    {
        if (_paymentMethods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodRefunded(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.DenyPaymentMethodRefund cmd)
    {
        if (_paymentMethods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodRefundDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.PaymentRequested @event)
    {
        (Id, OrderId, Amount, BillingAddress, var methods, Status) = @event;
        _paymentMethods.AddRange(methods.Select(method => (PaymentMethod) method));
    }

    private void When(DomainEvent.PaymentMethodAuthorized @event)
        => _paymentMethods.Single(method => method.Id == @event.PaymentMethodId).Authorize();

    private void When(DomainEvent.PaymentMethodDenied @event)
        => _paymentMethods.Single(method => method.Id == @event.PaymentMethodId).Deny();

    private void When(DomainEvent.PaymentMethodCanceled @event)
        => _paymentMethods.Single(method => method.Id == @event.PaymentMethodId).Cancel();

    private void When(DomainEvent.PaymentMethodRefunded @event)
        => _paymentMethods.Single(method => method.Id == @event.PaymentMethodId).Refund();

    private void When(DomainEvent.PaymentCompleted _)
        => Status = PaymentStatus.Completed;

    private void When(DomainEvent.PaymentNotCompleted _)
        => Status = PaymentStatus.Pending;
}
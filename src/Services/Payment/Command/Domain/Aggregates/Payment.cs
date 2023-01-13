using Domain.Abstractions.Aggregates;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Contracts.Abstractions.Messages;
using Contracts.Services.Payment;
using Domain.ValueObjects;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class Payment : AggregateRoot<PaymentValidator>
{
    [JsonProperty]
    private readonly List<PaymentMethod> _methods = new();

    public Guid OrderId { get; private set; }

    public Money Amount { get; private set; }
    public PaymentStatus? Status { get; private set; }
    public Address? BillingAddress { get; private set; }

    public decimal AmountDue
        => _methods
            .Where(method => method.Status != PaymentMethodStatus.Authorized)
            .Sum(method => method.Amount.Value);

    public IEnumerable<PaymentMethod> Methods
        => _methods.AsReadOnly();

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
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodAuthorized(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.DenyPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.CancelPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodCanceled(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.DenyPaymentMethodCancellation cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodCancellationDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.RefundPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodRefunded(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public void Handle(Command.DenyPaymentMethodRefund cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodRefundDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId));
    }

    public override void Handle(ICommand @command)
        => Handle(@command as dynamic);

    protected override void Apply(IEvent? @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.PaymentRequested @event)
    {
        (Id, OrderId, Amount, BillingAddress, var methods, Status) = @event;
        _methods.AddRange(methods.Select(method => (PaymentMethod)method));
    }

    private void Apply(DomainEvent.PaymentMethodAuthorized @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Authorize();

    private void Apply(DomainEvent.PaymentMethodDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Deny();

    private void Apply(DomainEvent.PaymentMethodCanceled @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Cancel();

    private void Apply(DomainEvent.PaymentMethodCancellationDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).DenyCancellation();

    private void Apply(DomainEvent.PaymentMethodRefunded @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Refund();

    private void Apply(DomainEvent.PaymentMethodRefundDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).DenyRefund();

    private void Apply(DomainEvent.PaymentCompleted _)
        => Status = PaymentStatus.Completed;

    private void Apply(DomainEvent.PaymentNotCompleted _)
        => Status = PaymentStatus.Pending;
}
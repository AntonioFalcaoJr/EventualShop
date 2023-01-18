using Domain.Abstractions.Aggregates;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
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

    public Money AmountDue => Amount with
    {
        Value = _methods
            .Where(method => method.Status is not PaymentMethodStatus.AuthorizedStatus)
            .Sum(method => method.Amount.Value)
    };

    public IEnumerable<PaymentMethod> Methods
        => _methods.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    public void Handle(Command.RequestPayment cmd)
    {
        RaiseEvent(new DomainEvent.PaymentRequested(Guid.NewGuid(), cmd.OrderId, cmd.AmountDue, cmd.BillingAddress, PaymentStatus.Ready));

        foreach (var method in cmd.PaymentMethods)
        {
            RaiseEvent(method.Option switch
            {
                Dto.CreditCard creditCard => new DomainEvent.CreditCardAdded(Id, Guid.NewGuid(), method.Amount, creditCard, PaymentMethodStatus.Pending),
                Dto.DebitCard debitCard => new DomainEvent.DebitCardAdded(Id, Guid.NewGuid(), method.Amount, debitCard, PaymentMethodStatus.Pending),
                Dto.PayPal payPal => new DomainEvent.PayPalAdded(Id, Guid.NewGuid(), method.Amount, payPal, PaymentMethodStatus.Pending),
                _ => throw new NotImplementedException()
            });
        }
    }

    public void Handle(Command.ProceedWithPayment cmd)
        => RaiseEvent(AmountDue == 0
            ? new DomainEvent.PaymentCompleted(cmd.PaymentId, cmd.OrderId, PaymentStatus.Completed)
            : new DomainEvent.PaymentNotCompleted(cmd.PaymentId, cmd.OrderId, PaymentStatus.NotCompleted));

    public void Handle(Command.CancelPayment cmd)
        => RaiseEvent(new DomainEvent.PaymentCanceled(cmd.PaymentId, cmd.OrderId, PaymentStatus.Cancelled));

    public void Handle(Command.AuthorizePaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodAuthorized(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Authorized));
    }

    public void Handle(Command.DenyPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Denied));
    }

    public void Handle(Command.CancelPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodCanceled(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Cancelled));
    }

    public void Handle(Command.DenyPaymentMethodCancellation cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodCancellationDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.CancellationDenied));
    }

    public void Handle(Command.RefundPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodRefunded(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Refunded));
    }

    public void Handle(Command.DenyPaymentMethodRefund cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(new DomainEvent.PaymentMethodRefundDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.RefundDenied));
    }

    protected override void Apply(IEvent? @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.PaymentRequested @event)
        => (Id, OrderId, Amount, BillingAddress, Status) = @event;

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

    private void Apply(DomainEvent.PaymentCompleted @event)
        => Status = @event.Status;

    private void Apply(DomainEvent.PaymentNotCompleted @event)
        => Status = @event.Status;
}
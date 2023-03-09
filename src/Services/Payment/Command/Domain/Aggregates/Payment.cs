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
    public Money Amount { get; private set; } = Money.Zero(Currency.Undefined);
    public PaymentStatus Status { get; private set; } = PaymentStatus.Empty;
    public Address BillingAddress { get; private set; } = default!;

    public Money AmountDue => Amount with
    {
        Amount = _methods
            .Where(method => method.Status is not PaymentMethodStatus.AuthorizedStatus)
            .Sum(method => method.Amount.Amount)
    };

    public IEnumerable<PaymentMethod> Methods
        => _methods.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    public void Handle(Command.RequestPayment cmd)
    {
        RaiseEvent<DomainEvent.PaymentRequested>(version
            => new(Guid.NewGuid(), cmd.OrderId, cmd.AmountDue, cmd.BillingAddress, PaymentStatus.Ready, version));

        foreach (var method in cmd.PaymentMethods)
        {
            RaiseEvent(version => method.Option switch
            {
                Dto.CreditCard creditCard => new DomainEvent.CreditCardAdded(Id, Guid.NewGuid(), method.Amount, creditCard, PaymentMethodStatus.Pending, version),
                Dto.DebitCard debitCard => new DomainEvent.DebitCardAdded(Id, Guid.NewGuid(), method.Amount, debitCard, PaymentMethodStatus.Pending, version),
                Dto.PayPal payPal => new DomainEvent.PayPalAdded(Id, Guid.NewGuid(), method.Amount, payPal, PaymentMethodStatus.Pending, version),
                _ => throw new NotImplementedException()
            });
        }
    }

    public void Handle(Command.ProceedWithPayment cmd)
        => RaiseEvent(version => AmountDue == 0
            ? new DomainEvent.PaymentCompleted(cmd.PaymentId, cmd.OrderId, PaymentStatus.Completed, version)
            : new DomainEvent.PaymentNotCompleted(cmd.PaymentId, cmd.OrderId, PaymentStatus.NotCompleted, version));

    public void Handle(Command.CancelPayment cmd)
        => RaiseEvent(version => new DomainEvent.PaymentCanceled(cmd.PaymentId, cmd.OrderId, PaymentStatus.Cancelled, version));

    public void Handle(Command.AuthorizePaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(version => new DomainEvent.PaymentMethodAuthorized(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Authorized, version));
    }

    public void Handle(Command.DenyPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId))
            RaiseEvent(version => new DomainEvent.PaymentMethodDenied(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Denied, version));
    }

    public void Handle(Command.CancelPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId) is false) return;

        RaiseEvent<DomainEvent.PaymentMethodCanceled>(version
            => new(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Cancelled, version));
    }

    public void Handle(Command.DenyPaymentMethodCancellation cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId) is false) return;

        RaiseEvent<DomainEvent.PaymentMethodCancellationDenied>(version
            => new(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.CancellationDenied, version));
    }

    public void Handle(Command.RefundPaymentMethod cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId) is false) return;

        RaiseEvent<DomainEvent.PaymentMethodRefunded>(version
            => new(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.Refunded, version));
    }

    public void Handle(Command.DenyPaymentMethodRefund cmd)
    {
        if (_methods.Exists(method => method.Id == cmd.PaymentMethodId) is false) return;

        RaiseEvent<DomainEvent.PaymentMethodRefundDenied>(version
            => new(cmd.PaymentId, cmd.PaymentMethodId, cmd.TransactionId, PaymentMethodStatus.RefundDenied, version));
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.PaymentRequested @event)
        => (Id, OrderId, Amount, BillingAddress, Status, _) = @event;

    private void When(DomainEvent.PaymentMethodAuthorized @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Authorize();

    private void When(DomainEvent.PaymentMethodDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Deny();

    private void When(DomainEvent.PaymentMethodCanceled @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Cancel();

    private void When(DomainEvent.PaymentMethodCancellationDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).DenyCancellation();

    private void When(DomainEvent.PaymentMethodRefunded @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Refund();

    private void When(DomainEvent.PaymentMethodRefundDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).DenyRefund();

    private void When(DomainEvent.PaymentCompleted @event)
        => Status = @event.Status;

    private void When(DomainEvent.PaymentNotCompleted @event)
        => Status = @event.Status;
}
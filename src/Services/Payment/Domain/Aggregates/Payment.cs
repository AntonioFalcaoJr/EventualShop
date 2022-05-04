using Domain.Abstractions.Aggregates;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;
using Domain.Entities.PaymentMethods.DebitCards;
using Domain.Entities.PaymentMethods.PayPal;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Payment;

namespace Domain.Aggregates;

public class Payment : AggregateRoot<Guid, PaymentValidator>
{
    private readonly List<IPaymentMethod> _methods = new();

    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public Address BillingAddress { get; private set; }

    public decimal AmountDue
        => _methods
            .Where(method => method.Status is not PaymentMethodStatus.Authorized)
            .Sum(method => method.Amount);

    public IEnumerable<IPaymentMethod> Methods
        => _methods;

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
        _methods.AddRange(@event.PaymentMethods
            .Select<Dto.IPaymentMethod, IPaymentMethod>(method
                => method switch
                {
                    Dto.CreditCard creditCard => (CreditCard) creditCard,
                    Dto.DebitCard debitCard => (DebitCard) debitCard,
                    Dto.PayPal payPal => (PayPal) payPal, 
                    _ => default
                }));

        Status = PaymentStatus.FromName(@event.Status);
    }

    private void When(DomainEvent.PaymentMethodAuthorized @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Authorize();

    private void When(DomainEvent.PaymentMethodDenied @event)
        => _methods.Single(method => method.Id == @event.PaymentMethodId).Deny();

    private void When(DomainEvent.PaymentCompleted _)
        => Status = PaymentStatus.Completed;

    private void When(DomainEvent.PaymentNotCompleted _)
        => Status = PaymentStatus.Pending;
}
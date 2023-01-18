using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class DomainEvent
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Dto.Address BillingAddress, string Status) : Message, IEvent;

    //public record PaymentRequested(Guid PaymentId, Guid OrderId, (decimal value, string symbol) Amount, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status) : Message, IEvent;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId, string Status) : Message, IEvent;

    public record PaymentCompleted(Guid PaymentId, Guid OrderId, string Status) : Message, IEvent;

    public record PaymentNotCompleted(Guid PaymentId, Guid OrderId, string Status) : Message, IEvent;

    public record PaymentMethodAuthorized(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, string Status) : Message, IEvent;

    public record PaymentMethodDenied(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, string Status) : Message, IEvent;

    public record PaymentMethodRefunded(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, string Status) : Message, IEvent;

    public record PaymentMethodRefundDenied(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, string Status) : Message, IEvent;

    public record PaymentMethodCancellationDenied(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, string Status) : Message, IEvent;

    public record PaymentMethodCanceled(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, string Status) : Message, IEvent;

    public record CreditCardAdded(Guid CartId, Guid MethodId, decimal Amount, Dto.CreditCard CreditCard, string Status) : Message, IEvent;

    public record DebitCardAdded(Guid CartId, Guid MethodId, decimal Amount, Dto.DebitCard DebitCard, string Status) : Message, IEvent;

    public record PayPalAdded(Guid CartId, Guid MethodId, decimal Amount, Dto.PayPal PayPal, string Status) : Message, IEvent;
}
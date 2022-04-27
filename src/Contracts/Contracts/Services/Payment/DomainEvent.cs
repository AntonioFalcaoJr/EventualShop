using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class DomainEvent
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Dto.Address BillingAddress, IEnumerable<Dto.IPaymentMethod> PaymentMethods, string Status) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentMethodAuthorized(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentMethodDenied(Guid PaymentId, Guid PaymentMethodId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentCompleted(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentNotCompleted(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), IEvent;
}
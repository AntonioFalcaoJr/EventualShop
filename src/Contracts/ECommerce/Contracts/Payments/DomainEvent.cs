using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Payments;

public static class DomainEvent
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Models.Address BillingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods, string Status) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentMethodAuthorized(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentMethodDenied(Guid PaymentId, Guid PaymentMethodId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentCompleted(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), IEvent;

    public record PaymentNotCompleted(Guid PaymentId, Guid OrderId) : Message(CorrelationId: PaymentId), IEvent;
}
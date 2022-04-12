using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Payments;

public static class DomainEvents
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Models.Address BillingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event(CorrelationId: PaymentId);

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Event(CorrelationId: PaymentId);

    public record PaymentMethodAuthorized(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Event(CorrelationId: PaymentId);

    public record PaymentMethodDenied(Guid PaymentId, Guid PaymentMethodId) : Event(CorrelationId: PaymentId);

    public record PaymentCompleted(Guid PaymentId, Guid OrderId) : Event(CorrelationId: PaymentId);

    public record PaymentNotCompleted(Guid PaymentId, Guid OrderId) : Event(CorrelationId: PaymentId);
}
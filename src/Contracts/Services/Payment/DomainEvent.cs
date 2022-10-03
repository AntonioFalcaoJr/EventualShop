using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Payment;

public static class DomainEvent
{
    public record PaymentRequested(Guid Id, Guid OrderId, decimal Amount, Dto.Address BillingAddress, IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status) : Message(CorrelationId: Id), IEvent;

    public record PaymentCanceled(Guid Id, Guid OrderId) : Message(CorrelationId: Id), IEvent;

    public record PaymentCompleted(Guid Id, Guid OrderId) : Message(CorrelationId: Id), IEvent;

    public record PaymentNotCompleted(Guid Id, Guid OrderId) : Message(CorrelationId: Id), IEvent;

    public record PaymentMethodAuthorized(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), IEvent;

    public record PaymentMethodDenied(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), IEvent;

    public record PaymentMethodRefunded(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), IEvent;

    public record PaymentMethodRefundDenied(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), IEvent;

    public record PaymentMethodCancellationDenied(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), IEvent;

    public record PaymentMethodCanceled(Guid Id, Guid PaymentMethodId, Guid TransactionId) : Message(CorrelationId: Id), IEvent;
}
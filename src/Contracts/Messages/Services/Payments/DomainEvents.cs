using System;
using Messages.Abstractions.Events;

namespace Messages.Services.Payments;

public static class DomainEvents
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Models.Address BillingAddress, Models.CreditCard CreditCard) : Event;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Event;

    public record PaymentMethodAuthorized(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Event;

    public record PaymentMethodDenied(Guid PaymentId, Guid PaymentMethodId) : Event;

    public record PaymentProceeded(Guid PaymentId, Guid OrderId) : Event;

    public record PaymentCompleted(Guid PaymentId, Guid OrderId) : Event;

    public record PaymentNotCompleted(Guid PaymentId, Guid OrderId) : Event;
}
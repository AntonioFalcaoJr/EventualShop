using System;
using System.Collections.Generic;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Payment;

public static class DomainEvents
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Models.Address BillingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Event;

    public record PaymentMethodAuthorized(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId) : Event;

    public record PaymentMethodDenied(Guid PaymentId, Guid PaymentMethodId) : Event;

    public record PaymentCompleted(Guid PaymentId, Guid OrderId) : Event;

    public record PaymentNotCompleted(Guid PaymentId, Guid OrderId) : Event;
}
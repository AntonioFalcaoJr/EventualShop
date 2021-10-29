using System;
using Messages.Abstractions.Events;

namespace Messages.Payments;

public static class Events
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Models.Address BillingAddress, Models.CreditCard CreditCard) : Event;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Event;
}
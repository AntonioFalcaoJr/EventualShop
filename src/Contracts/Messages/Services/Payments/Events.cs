using System;
using Messages.Abstractions.Events;

namespace Messages.Services.Payments;

public static class DomainEvents
{
    public record PaymentRequested(Guid PaymentId, Guid OrderId, decimal Amount, Models.Address BillingAddress, Models.CreditCard CreditCard) : Event;

    public record PaymentCanceled(Guid PaymentId, Guid OrderId) : Event;
}
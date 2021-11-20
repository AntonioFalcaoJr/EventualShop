using System;
using Messages.Abstractions.Commands;

namespace Messages.Services.Payments;

public static class Commands
{
    public record RequestPayment(Guid OrderId, decimal Amount, Models.Address BillingAddress, Models.CreditCard CreditCard) : Command;

    public record CancelPayment(Guid PaymentId, Guid OrderId) : Command;

    public record UpdatePaymentMethod(Guid PaymentId, Guid PaymentMethodId, Guid TransactionId, bool Authorized) : Command;

    public record ProceedWithPayment(Guid PaymentId, Guid OrderId) : Command;
}
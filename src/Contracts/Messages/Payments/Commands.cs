using System;
using Messages.Abstractions.Commands;

namespace Messages.Payments
{
    public static class Commands
    {
        public record RequestPayment(Guid OrderId, decimal Amount, Models.Address BillingAddress, Models.CreditCard CreditCard) : Command;
        public record CancelPayment(Guid PaymentId, Guid OrderId) : Command;
    }
}
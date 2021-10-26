using System;
using Messages.Abstractions.Queries;

namespace Messages.Payments
{
    public static class Queries
    {
        public record GetPaymentDetails(Guid PaymentId) : Query;
    }
}
using System;
using Messages.Abstractions.Queries.Responses;

namespace Messages.Payments;

public static class Responses
{
    public record PaymentDetails : Response
    {
        public Guid Id { get; init; }
        public Guid OrderId { get; init; }
        public decimal Amount { get; init; }
        public string Status { get; init; }
        public bool IsDeleted { get; init; }
    }
}
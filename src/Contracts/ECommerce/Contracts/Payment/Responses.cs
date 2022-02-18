using System;
using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Payment;

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
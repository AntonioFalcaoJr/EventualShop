using System;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Payment;

public static class Queries
{
    public record GetPaymentDetails(Guid PaymentId) : Query(CorrelationId: PaymentId);
}
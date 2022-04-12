using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Payments;

public static class Queries
{
    public record GetPaymentDetails(Guid PaymentId) : Query(CorrelationId: PaymentId);
}
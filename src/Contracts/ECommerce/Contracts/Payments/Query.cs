using ECommerce.Abstractions;

namespace ECommerce.Contracts.Payments;

public static class Query
{
    public record GetPaymentDetails(Guid PaymentId) : Message(CorrelationId: PaymentId), IQuery;
}
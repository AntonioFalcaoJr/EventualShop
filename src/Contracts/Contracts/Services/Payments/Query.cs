using Contracts.Abstractions;

namespace Contracts.Services.Payments;

public static class Query
{
    public record GetPaymentDetails(Guid PaymentId) : Message(CorrelationId: PaymentId), IQuery;
}
using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Payments;

public static class Query
{
    public record GetPaymentDetails(Guid PaymentId) : Message(CorrelationId: PaymentId), IQuery;
}
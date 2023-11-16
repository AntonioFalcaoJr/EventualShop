using Application.Abstractions;
using Contracts.Boundaries.Payment;

namespace Application.UseCases.Queries;

public class GetPaymentDetailsInteractor(IProjectionGateway<Projection.PaymentDetails> projectionGateway)
    : IInteractor<Query.GetPaymentDetails, Projection.PaymentDetails>
{
    public async Task<Projection.PaymentDetails?> InteractAsync(Query.GetPaymentDetails query, CancellationToken cancellationToken)
        => await projectionGateway.GetAsync(query.PaymentId, cancellationToken);
}
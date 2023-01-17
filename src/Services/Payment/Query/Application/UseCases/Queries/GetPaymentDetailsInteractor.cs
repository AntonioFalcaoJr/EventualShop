using Application.Abstractions;
using Contracts.Services.Payment;

namespace Application.UseCases.Queries;

public class GetPaymentDetailsInteractor : IInteractor<Query.GetPaymentDetails, Projection.PaymentDetails>
{
    private readonly IProjectionGateway<Projection.PaymentDetails> _projectionGateway;

    public GetPaymentDetailsInteractor(IProjectionGateway<Projection.PaymentDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task<Projection.PaymentDetails?> InteractAsync(Query.GetPaymentDetails query, CancellationToken cancellationToken)
        => await _projectionGateway.GetAsync(query.PaymentId, cancellationToken);
}
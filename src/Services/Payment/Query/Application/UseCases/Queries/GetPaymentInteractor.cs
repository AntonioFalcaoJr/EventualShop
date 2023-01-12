using Application.Abstractions;
using Contracts.Services.Payment;

namespace Application.UseCases.Queries;

public class GetPaymentInteractor : IInteractor<Query.GetPayment, Projection.Payment>
{
    private readonly IProjectionGateway<Projection.Payment> _projectionGateway;

    public GetPaymentInteractor(IProjectionGateway<Projection.Payment> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task<Projection.Payment?> InteractAsync(Query.GetPayment query, CancellationToken cancellationToken)
        => await _projectionGateway.GetAsync(query.PaymentId, cancellationToken);
}
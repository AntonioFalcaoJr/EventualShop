using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetPaymentMethodDetailsInteractor : IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails>
{
    private readonly IProjectionGateway<Projection.PaymentMethodDetails> _projectionGateway;

    public GetPaymentMethodDetailsInteractor(IProjectionGateway<Projection.PaymentMethodDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.PaymentMethodDetails?> InteractAsync(Query.GetPaymentMethodDetails query, CancellationToken cancellationToken)
        => _projectionGateway.FindAsync(method => method.Id == query.MethodId && method.CartId == query.CartId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetPaymentMethodDetailsInteractor(IProjectionGateway<Projection.PaymentMethodDetails> projectionGateway)
    : IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails>
{
    public Task<Projection.PaymentMethodDetails?> InteractAsync(Query.GetPaymentMethodDetails query, CancellationToken cancellationToken)
        => projectionGateway.FindAsync(method => method.Id == query.MethodId && method.CartId == query.CartId, cancellationToken);
}
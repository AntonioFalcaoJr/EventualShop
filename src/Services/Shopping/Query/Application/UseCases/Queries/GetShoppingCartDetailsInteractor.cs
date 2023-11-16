using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetShoppingCartDetailsInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    : IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails>
{
    public Task<Projection.ShoppingCartDetails?> InteractAsync(Query.GetShoppingCartDetails query, CancellationToken cancellationToken)
        => projectionGateway.GetAsync(query.CartId, cancellationToken);
}
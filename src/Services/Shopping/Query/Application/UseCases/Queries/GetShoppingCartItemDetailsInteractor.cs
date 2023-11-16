using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetShoppingCartItemDetailsInteractor(IProjectionGateway<Projection.ShoppingCartItemDetails> projectionGateway)
    : IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails>
{
    public Task<Projection.ShoppingCartItemDetails?> InteractAsync(Query.GetShoppingCartItemDetails query, CancellationToken cancellationToken)
        => projectionGateway.FindAsync(item => item.Id == query.ItemId && item.CartId == query.CartId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetShoppingCartItemDetailsInteractor : IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails>
{
    private readonly IProjectionGateway<Projection.ShoppingCartItemDetails> _projectionGateway;

    public GetShoppingCartItemDetailsInteractor(IProjectionGateway<Projection.ShoppingCartItemDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.ShoppingCartItemDetails?> InteractAsync(Query.GetShoppingCartItemDetails query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.ItemId, cancellationToken);
}
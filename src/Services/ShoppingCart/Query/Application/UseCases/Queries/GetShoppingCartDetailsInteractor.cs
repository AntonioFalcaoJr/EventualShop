using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetShoppingCartDetailsInteractor : IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails>
{
    private readonly IProjectionGateway<Projection.ShoppingCartDetails> _projectionGateway;

    public GetShoppingCartDetailsInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.ShoppingCartDetails?> InteractAsync(Query.GetShoppingCartDetails query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.CartId, cancellationToken);
}
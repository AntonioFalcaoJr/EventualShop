using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetCustomerShoppingCartDetailsInteractor : IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails>
{
    private readonly IProjectionGateway<Projection.ShoppingCartDetails> _projectionGateway;

    public GetCustomerShoppingCartDetailsInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.ShoppingCartDetails?> InteractAsync(Query.GetCustomerShoppingCartDetails query, CancellationToken cancellationToken)
        => _projectionGateway.FindAsync(cart => cart.Id == query.CartId && cart.CustomerId == query.CustomerId, cancellationToken);
}
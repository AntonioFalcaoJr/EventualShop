using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Queries;

public class GetCustomerShoppingCartDetailsInteractor(IProjectionGateway<Projection.ShoppingCartDetails> projectionGateway)
    : IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails>
{
    public Task<Projection.ShoppingCartDetails?> InteractAsync(Query.GetCustomerShoppingCartDetails query, CancellationToken cancellationToken)
        => projectionGateway.FindAsync(cart => cart.CustomerId == query.CustomerId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class ListShoppingCartItemsListItemsInteractor : IPagedInteractor<Query.ListShoppingCartItemsListItems, Projection.ShoppingCartItemListItem>
{
    private readonly IProjectionGateway<Projection.ShoppingCartItemListItem> _projectionGateway;

    public ListShoppingCartItemsListItemsInteractor(IProjectionGateway<Projection.ShoppingCartItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.ShoppingCartItemListItem>> InteractAsync(Query.ListShoppingCartItemsListItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, item => item.CartId == query.CartId, cancellationToken);
}
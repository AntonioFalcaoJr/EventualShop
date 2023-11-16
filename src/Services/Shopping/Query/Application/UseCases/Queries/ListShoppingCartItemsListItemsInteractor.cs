using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Queries;

public class ListShoppingCartItemsListItemsInteractor(IProjectionGateway<Projection.ShoppingCartItemListItem> projectionGateway)
    : IPagedInteractor<Query.ListShoppingCartItemsListItems, Projection.ShoppingCartItemListItem>
{
    public ValueTask<IPagedResult<Projection.ShoppingCartItemListItem>> InteractAsync(Query.ListShoppingCartItemsListItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, item => item.CartId == query.CartId, cancellationToken);
}
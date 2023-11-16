using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Warehouse;

namespace Application.UseCases.Queries;

public class ListInventoryItemsListItemsInteractor(IProjectionGateway<Projection.InventoryItemListItem> projectionGateway)
    : IPagedInteractor<Query.ListInventoryItemsListItems, Projection.InventoryItemListItem>
{
    public ValueTask<IPagedResult<Projection.InventoryItemListItem>> InteractAsync(Query.ListInventoryItemsListItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, item => item.InventoryId == query.InventoryId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Queries;

public class ListInventoryItemsInteractor : IInteractor<Query.ListInventoryItemsListItems, IPagedResult<Projection.InventoryItemListItem>>
{
    private readonly IProjectionGateway<Projection.InventoryItemListItem> _projectionGateway;

    public ListInventoryItemsInteractor(IProjectionGateway<Projection.InventoryItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.InventoryItemListItem>?> InteractAsync(Query.ListInventoryItemsListItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, item => item.InventoryId == query.InventoryId, cancellationToken);
}
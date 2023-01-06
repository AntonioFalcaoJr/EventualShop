using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Queries;

public class ListInventoryItemsInteractor : IInteractor<Query.ListInventoryItems, IPagedResult<Projection.InventoryItem>>
{
    private readonly IProjectionGateway<Projection.InventoryItem> _projectionGateway;

    public ListInventoryItemsInteractor(IProjectionGateway<Projection.InventoryItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.InventoryItem>?> InteractAsync(Query.ListInventoryItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, item => item.InventoryId == query.InventoryId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Warehouse;

namespace Application.UseCases.Queries;

public class ListInventoriesGridItemInteractor(IProjectionGateway<Projection.InventoryGridItem> projectionGateway)
    : IPagedInteractor<Query.ListInventoryGridItems, Projection.InventoryGridItem>
{
    public ValueTask<IPagedResult<Projection.InventoryGridItem>> InteractAsync(Query.ListInventoryGridItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, cancellationToken);
}
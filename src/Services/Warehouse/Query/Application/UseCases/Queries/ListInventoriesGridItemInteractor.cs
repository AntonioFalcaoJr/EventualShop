using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Queries;

public class ListInventoriesGridItemInteractor : IPagedInteractor<Query.ListInventoryGridItems, Projection.InventoryGridItem>
{
    private readonly IProjectionGateway<Projection.InventoryGridItem> _projectionGateway;

    public ListInventoriesGridItemInteractor(IProjectionGateway<Projection.InventoryGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.InventoryGridItem>> InteractAsync(Query.ListInventoryGridItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}
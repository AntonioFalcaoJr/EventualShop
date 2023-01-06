using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Queries;

public class ListInventoriesGridInteractor : IInteractor<Query.ListInventoryGridItems, IPagedResult<Projection.Inventory>>
{
    private readonly IProjectionGateway<Projection.Inventory> _projectionGateway;

    public ListInventoriesGridInteractor(IProjectionGateway<Projection.Inventory> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.Inventory>?> InteractAsync(Query.ListInventoryGridItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}
using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogsGridItemsInteractor : IInteractor<Query.ListCatalogsGridItems, IPagedResult<Projection.CatalogGridItem>>
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ListCatalogsGridItemsInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogGridItem>?> InteractAsync(Query.ListCatalogsGridItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}
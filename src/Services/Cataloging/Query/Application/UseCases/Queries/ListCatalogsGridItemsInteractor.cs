using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Cataloging.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogsGridItemsInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    : IPagedInteractor<Query.ListCatalogsGridItems, Projection.CatalogGridItem>
{
    public ValueTask<IPagedResult<Projection.CatalogGridItem>> InteractAsync(Query.ListCatalogsGridItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, cancellationToken);
}
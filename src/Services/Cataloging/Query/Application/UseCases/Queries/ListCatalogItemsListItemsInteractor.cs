using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Cataloging.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogItemsListItemsInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    : IPagedInteractor<Query.ListCatalogItemsListItems, Projection.CatalogItemListItem>
{
    public ValueTask<IPagedResult<Projection.CatalogItemListItem>> InteractAsync(Query.ListCatalogItemsListItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, listItem => listItem.CatalogId == query.CatalogId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogItemsListItemsInteractor : IPagedInteractor<Query.ListCatalogItemsListItems, Projection.CatalogItemListItem>
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ListCatalogItemsListItemsInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.CatalogItemListItem>> InteractAsync(Query.ListCatalogItemsListItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, listItem => listItem.CatalogId == query.CatalogId, cancellationToken);
}
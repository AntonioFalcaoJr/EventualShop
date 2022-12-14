using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public interface IListCatalogItemsListItemsInteractor : IInteractor<Query.ListCatalogItemsListItems, IPagedResult<Projection.CatalogItemListItem>> { }

public class ListCatalogItemsListItemsInteractor : IListCatalogItemsListItemsInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ListCatalogItemsListItemsInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogItemListItem>> InteractAsync(Query.ListCatalogItemsListItems query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, listItem => listItem.CatalogId == query.CatalogId, cancellationToken);
}
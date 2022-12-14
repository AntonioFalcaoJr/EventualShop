using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogItemsInteractor : IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItemListItem>>
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ListCatalogItemsInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogItemListItem>> InteractAsync(Query.GetAllItems query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}
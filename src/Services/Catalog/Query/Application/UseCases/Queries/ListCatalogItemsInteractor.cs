using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogItemsInteractor : IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItem>>
{
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGateway;

    public ListCatalogItemsInteractor(IProjectionGateway<Projection.CatalogItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogItem>> InteractAsync(Query.GetAllItems query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}
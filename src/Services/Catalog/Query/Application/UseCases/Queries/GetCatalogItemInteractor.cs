using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class GetCatalogItemInteractor : IInteractor<Query.GetCatalogItems, Projection.CatalogItemListItem>
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public GetCatalogItemInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.CatalogItemListItem> InteractAsync(Query.GetCatalogItems query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.CatalogId, cancellationToken);
}
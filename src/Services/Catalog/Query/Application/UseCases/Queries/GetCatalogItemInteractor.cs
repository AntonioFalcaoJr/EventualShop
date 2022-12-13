using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class GetCatalogItemInteractor : IInteractor<Query.GetCatalogItems, Projection.CatalogItem>
{
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGateway;

    public GetCatalogItemInteractor(IProjectionGateway<Projection.CatalogItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.CatalogItem> InteractAsync(Query.GetCatalogItems query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.CatalogId, cancellationToken);
}
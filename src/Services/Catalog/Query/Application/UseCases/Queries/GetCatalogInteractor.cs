using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class GetCatalogInteractor : IInteractor<Query.GetCatalog, Projection.CatalogDetails>
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public GetCatalogInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.CatalogDetails> InteractAsync(Query.GetCatalog query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.CatalogId, cancellationToken);
}
using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class GetCatalogInteractor : IInteractor<Query.GetCatalog, Projection.Catalog>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public GetCatalogInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
       => _projectionGateway = projectionGateway;

    public Task<Projection.Catalog> InteractAsync(Query.GetCatalog query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.CatalogId, cancellationToken);
}
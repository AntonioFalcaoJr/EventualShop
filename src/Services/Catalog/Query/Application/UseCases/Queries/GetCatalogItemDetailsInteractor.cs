using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class GetCatalogItemDetailsInteractor : IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails>
{
    private readonly IProjectionGateway<Projection.CatalogItemDetails> _projectionGateway;

    public GetCatalogItemDetailsInteractor(IProjectionGateway<Projection.CatalogItemDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.CatalogItemDetails> InteractAsync(Query.GetCatalogItemDetails query, CancellationToken cancellationToken)
        => _projectionGateway.FindAsync(item => item.CatalogId == query.CatalogId && item.Id == query.ItemId, cancellationToken);
}
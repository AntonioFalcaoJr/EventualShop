using Application.Abstractions;
using Contracts.Boundaries.Cataloging.Catalog;

namespace Application.UseCases.Queries;

public class GetCatalogItemDetailsInteractor(IProjectionGateway<Projection.CatalogItemDetails> projectionGateway)
    : IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails>
{
    public Task<Projection.CatalogItemDetails?> InteractAsync(Query.GetCatalogItemDetails query, CancellationToken cancellationToken)
        => projectionGateway.FindAsync(item => item.CatalogId == query.CatalogId && item.Id == query.ItemId, cancellationToken);
}
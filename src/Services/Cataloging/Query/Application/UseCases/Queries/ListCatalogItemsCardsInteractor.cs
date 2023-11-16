using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Cataloging.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogItemsCardsInteractor(IProjectionGateway<Projection.CatalogItemCard> projectionGateway)
    : IPagedInteractor<Query.ListCatalogItemsCards, Projection.CatalogItemCard>
{
    public ValueTask<IPagedResult<Projection.CatalogItemCard>> InteractAsync(Query.ListCatalogItemsCards query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, card => card.CatalogId == query.CatalogId, cancellationToken);
}
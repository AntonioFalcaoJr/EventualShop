using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogItemsCardsInteractor : IInteractor<Query.ListCatalogItemsCards, IPagedResult<Projection.CatalogItemCard>>
{
    private readonly IProjectionGateway<Projection.CatalogItemCard> _projectionGateway;

    public ListCatalogItemsCardsInteractor(IProjectionGateway<Projection.CatalogItemCard> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogItemCard>?> InteractAsync(Query.ListCatalogItemsCards query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, card => card.CatalogId == query.CatalogId, cancellationToken);
}
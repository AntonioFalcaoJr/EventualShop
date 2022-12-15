using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public interface IListCatalogItemsCardsInteractor : IInteractor<Query.ListCatalogItemsCards, IPagedResult<Projection.CatalogItemCard>> { }

public class ListCatalogItemsCardsInteractor : IListCatalogItemsCardsInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemCard> _projectionGateway;

    public ListCatalogItemsCardsInteractor(IProjectionGateway<Projection.CatalogItemCard> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogItemCard>> InteractAsync(Query.ListCatalogItemsCards query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, card => card.CatalogId == query.CatalogId, cancellationToken);
}
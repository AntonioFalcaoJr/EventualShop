using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public interface IListCatalogsGridItemsInteractor : IInteractor<Query.ListCatalogsGridItems, IPagedResult<Projection.CatalogGridItem>> { }

public class ListCatalogsGridItemsInteractor : IListCatalogsGridItemsInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ListCatalogsGridItemsInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogGridItem>> InteractAsync(Query.ListCatalogsGridItems query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}
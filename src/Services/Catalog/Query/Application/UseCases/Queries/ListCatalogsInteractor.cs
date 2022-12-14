using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogsInteractor : IInteractor<Query.GetCatalogs, IPagedResult<Projection.CatalogDetails>>
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public ListCatalogsInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.CatalogDetails>> InteractAsync(Query.GetCatalogs query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}
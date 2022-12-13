using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.UseCases.Queries;

public class ListCatalogsInteractor : IInteractor<Query.GetCatalogs, IPagedResult<Projection.Catalog>>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public ListCatalogsInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
        => _projectionGateway = projectionGateway;

    public Task<IPagedResult<Projection.Catalog>> InteractAsync(Query.GetCatalogs query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;
using Infrastructure.Abstractions.EventSourcing.Projections.Pagination;

namespace Infrastructure.EventSourcing.Projections;

public class CatalogProjectionsService : ICatalogProjectionsService
{
    private readonly ICatalogProjectionsRepository _repository;

    public CatalogProjectionsService(ICatalogProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<IPagedResult<CatalogProjection>> GetCatalogsAsync(int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<CatalogProjection>(
            paging: new Paging { Limit = limit, Offset = offset },
            predicate: projection => projection.IsActive && projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<IPagedResult<CatalogItemProjection>> GetCatalogItemsAsync(Guid catalogId, int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<CatalogItemProjection>(
            paging: new Paging { Limit = limit, Offset = offset },
            predicate: projection => projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<CatalogProjection> GetCatalogDetailsAsync(Guid catalogId, CancellationToken cancellationToken)
        => _repository.GetAsync<CatalogProjection, Guid>(catalogId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}
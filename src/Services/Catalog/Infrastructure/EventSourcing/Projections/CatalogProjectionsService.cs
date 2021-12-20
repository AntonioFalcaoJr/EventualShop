using System.Linq.Expressions;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class CatalogProjectionsService : ICatalogProjectionsService
{
    private readonly ICatalogProjectionsRepository _repository;

    public CatalogProjectionsService(ICatalogProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<IPagedResult<CatalogProjection>> GetCatalogsDetailsWithPaginationAsync(Paging paging, Expression<Func<CatalogProjection, bool>> predicate, CancellationToken cancellationToken)
        => _repository.GetAllAsync(paging, predicate, cancellationToken);

    public Task<IPagedResult<CatalogItemProjection>> GetCatalogItemsWithPaginationAsync(Paging paging, Expression<Func<CatalogProjection, bool>> predicate, Expression<Func<CatalogProjection, IEnumerable<CatalogItemProjection>>> selector, CancellationToken cancellationToken)
        => _repository.GetAllNestedAsync(paging, predicate, selector, cancellationToken);

    public Task<CatalogProjection> GetCatalogDetailsAsync(Guid accountId, CancellationToken cancellationToken)
        => _repository.GetAsync<CatalogProjection, Guid>(accountId, cancellationToken);
    
    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}
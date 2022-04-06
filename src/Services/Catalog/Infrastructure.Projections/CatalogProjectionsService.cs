using System.Linq.Expressions;
using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;
using ECommerce.Abstractions.Projections;
using Infrastructure.Projections.Abstractions.Pagination;

namespace Infrastructure.Projections;

public class CatalogProjectionsService : ICatalogProjectionsService
{
    private readonly ICatalogProjectionsRepository _repository;

    public CatalogProjectionsService(ICatalogProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<IPagedResult<ECommerce.Contracts.Catalogs.Projections.Catalog>> GetCatalogsAsync(int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<ECommerce.Contracts.Catalogs.Projections.Catalog>(
            paging: new Paging {Limit = limit, Offset = offset},
            predicate: projection => projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<IPagedResult<ECommerce.Contracts.Catalogs.Projections.CatalogItem>> GetCatalogItemsAsync(Guid catalogId, int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<ECommerce.Contracts.Catalogs.Projections.CatalogItem>(
            paging: new Paging {Limit = limit, Offset = offset},
            predicate: projection => projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<ECommerce.Contracts.Catalogs.Projections.Catalog> GetCatalogAsync(Guid catalogId, CancellationToken cancellationToken)
        => _repository.GetAsync<ECommerce.Contracts.Catalogs.Projections.Catalog, Guid>(catalogId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task ProjectManyAsync<TProjection>(IEnumerable<TProjection> projections, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertManyAsync(projections, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}
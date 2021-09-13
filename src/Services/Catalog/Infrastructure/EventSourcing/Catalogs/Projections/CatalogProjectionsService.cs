using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Catalogs.Projections;

namespace Infrastructure.EventSourcing.Catalogs.Projections
{
    public class CatalogProjectionsService : ICatalogProjectionsService
    {
        private readonly ICatalogProjectionsRepository _repository;

        public CatalogProjectionsService(ICatalogProjectionsRepository repository)
        {
            _repository = repository;
        }

        public Task<IPagedResult<CatalogProjection>> GetCatalogsDetailsWithPaginationAsync(Paging paging, Expression<Func<CatalogProjection, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(paging, predicate, cancellationToken);

        public Task ProjectNewCatalogDetailsAsync(CatalogProjection catalog, CancellationToken cancellationToken)
            => _repository.SaveAsync(catalog, cancellationToken);

        public Task<CatalogProjection> GetCatalogDetailsAsync(Guid accountId, CancellationToken cancellationToken)
            => _repository.GetAsync<CatalogProjection, Guid>(accountId, cancellationToken);
    }
}
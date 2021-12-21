using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections;

public interface ICatalogProjectionsService : IProjectionsService
{
    Task<IPagedResult<CatalogProjection>> GetCatalogsDetailsWithPaginationAsync(Paging paging, Expression<Func<CatalogProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<IPagedResult<CatalogItemProjection>> GetCatalogItemsWithPaginationAsync(Paging paging, Expression<Func<CatalogProjection, bool>> predicate, Expression<Func<CatalogProjection, IEnumerable<CatalogItemProjection>>> selector, CancellationToken cancellationToken);
    Task<CatalogProjection> GetCatalogDetailsAsync(Guid accountId, CancellationToken cancellationToken);
}
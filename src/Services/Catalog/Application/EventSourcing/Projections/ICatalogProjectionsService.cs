using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface ICatalogProjectionsService : IProjectionsService
{
    Task<IPagedResult<CatalogProjection>> GetCatalogsAsync(IPaging paging, CancellationToken cancellationToken);
    Task<IPagedResult<CatalogItemProjection>> GetCatalogItemsAsync(Guid catalogId, IPaging paging, CancellationToken cancellationToken);
    Task<CatalogProjection> GetCatalogDetailsAsync(Guid catalogId, CancellationToken cancellationToken);
}
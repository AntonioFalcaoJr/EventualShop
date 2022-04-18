using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface ICatalogProjectionsService : IProjectionsService
{
    Task<IPagedResult<ECommerce.Contracts.Catalogs.Projections.Catalog>> GetCatalogsAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<IPagedResult<ECommerce.Contracts.Catalogs.Projections.CatalogItem>> GetCatalogItemsAsync(Guid catalogId, int limit, int offset, CancellationToken cancellationToken);
    Task<ECommerce.Contracts.Catalogs.Projections.Catalog> GetCatalogAsync(Guid catalogId, CancellationToken cancellationToken);
    public Task RemoveCatalogAsync(Guid catalogId, CancellationToken cancellationToken);
    public Task RemoveItemsAsync(Guid catalogId, CancellationToken cancellationToken);
    public Task RemoveItemAsync(Guid itemId, CancellationToken cancellationToken);
}
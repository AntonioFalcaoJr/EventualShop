using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Abstractions.Pagination;

namespace Infrastructure.Projections;

public class CatalogProjectionsService : ProjectionsService, ICatalogProjectionsService
{
    public CatalogProjectionsService(ICatalogProjectionsRepository repository)
        : base(repository) { }

    public Task<IPagedResult<ECommerce.Contracts.Catalogs.Projections.Catalog>> GetCatalogsAsync(int limit, int offset, CancellationToken cancellationToken)
        => Repository.GetAllAsync<ECommerce.Contracts.Catalogs.Projections.Catalog>(
            paging: new Paging {Limit = limit, Offset = offset},
            predicate: projection => projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<IPagedResult<ECommerce.Contracts.Catalogs.Projections.CatalogItem>> GetCatalogItemsAsync(Guid catalogId, int limit, int offset, CancellationToken cancellationToken)
        => Repository.GetAllAsync<ECommerce.Contracts.Catalogs.Projections.CatalogItem>(
            paging: new Paging {Limit = limit, Offset = offset},
            predicate: projection => projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<ECommerce.Contracts.Catalogs.Projections.Catalog> GetCatalogAsync(Guid catalogId, CancellationToken cancellationToken)
        => Repository.GetAsync<ECommerce.Contracts.Catalogs.Projections.Catalog, Guid>(catalogId, cancellationToken);

    public Task RemoveCatalogAsync(Guid catalogId, CancellationToken cancellationToken)
        => Repository.DeleteAsync<ECommerce.Contracts.Catalogs.Projections.Catalog>(catalog => catalog.Id == catalogId, cancellationToken);

    public Task RemoveItemsAsync(Guid catalogId, CancellationToken cancellationToken)
        => Repository.DeleteAsync<ECommerce.Contracts.Catalogs.Projections.CatalogItem>(item => item.CatalogId == catalogId, cancellationToken);

    public Task RemoveItemAsync(Guid itemId, CancellationToken cancellationToken)
        => Repository.DeleteAsync<ECommerce.Contracts.Catalogs.Projections.CatalogItem>(item => item.Id == itemId, cancellationToken);
}
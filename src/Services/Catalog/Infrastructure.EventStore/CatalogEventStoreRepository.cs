using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class CatalogEventStoreRepository : EventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid>, ICatalogEventStoreRepository
{
    public CatalogEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
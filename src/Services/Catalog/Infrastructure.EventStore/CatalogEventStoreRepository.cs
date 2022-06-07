using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class CatalogEventStoreRepository : EventStoreRepository<Catalog, StoreEvents.Event, StoreEvents.Snapshot, Guid>, ICatalogEventStoreRepository
{
    public CatalogEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
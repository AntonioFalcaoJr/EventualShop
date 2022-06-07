using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class WarehouseEventStoreRepository : EventStoreRepository<Inventory, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IWarehouseEventStoreRepository
{
    public WarehouseEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class WarehouseEventStoreRepository : EventStoreRepository<Inventory, InventoryStoreEvent, InventorySnapshot>, IWarehouseEventStoreRepository
{
    public WarehouseEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
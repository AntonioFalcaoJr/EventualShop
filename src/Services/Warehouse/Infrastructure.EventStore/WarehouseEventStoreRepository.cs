using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class WarehouseEventStoreRepository : EventStoreRepository<InventoryItem, WarehouseStoreEvent, WarehouseSnapshot, Guid>, IWarehouseEventStoreRepository
{
    public WarehouseEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
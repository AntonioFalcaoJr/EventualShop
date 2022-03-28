using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore;

public class WarehouseEventStoreRepository : EventStoreRepository<InventoryItem, WarehouseStoreEvent, WarehouseSnapshot, Guid>, IWarehouseEventStoreRepository
{
    public WarehouseEventStoreRepository(EventStoreDbContext dbContext) 
        : base(dbContext) { }
}
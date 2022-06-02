using Application.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class WarehouseEventStoreRepository : EventStoreRepository<Inventory, WarehouseStoreEvent, WarehouseSnapshot, Guid>, IWarehouseEventStoreRepository
{
    public WarehouseEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
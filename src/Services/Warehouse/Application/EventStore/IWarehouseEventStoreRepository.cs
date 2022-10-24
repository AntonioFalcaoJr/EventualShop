using Application.Abstractions.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;

namespace Application.EventStore;

public interface IWarehouseEventStoreRepository : IEventStoreRepository<Inventory, InventoryStoreEvent, InventorySnapshot> { }
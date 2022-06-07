using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IWarehouseEventStoreRepository : IEventStoreRepository<Inventory, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }
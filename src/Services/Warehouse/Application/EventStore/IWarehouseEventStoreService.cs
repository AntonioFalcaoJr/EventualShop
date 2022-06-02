using Application.Abstractions.EventStore;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IWarehouseEventStoreService : IEventStoreService<Inventory, Guid> { }
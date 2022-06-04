using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IShoppingCartEventStoreRepository : IEventStoreRepository<ShoppingCart, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }
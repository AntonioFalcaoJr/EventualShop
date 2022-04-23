using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IShoppingCartEventStoreRepository : IEventStoreRepository<ShoppingCart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid> { }
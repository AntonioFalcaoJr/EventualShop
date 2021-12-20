using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface IShoppingCartEventStoreRepository : IEventStoreRepository<Product, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid> { }
using Application.Abstractions.EventStore;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface IShoppingCartEventStoreService : IEventStoreService<ShoppingCart, Guid> { }
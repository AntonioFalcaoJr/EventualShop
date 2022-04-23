using Application.Abstractions.EventStore;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IShoppingCartEventStoreService : IEventStoreService<ShoppingCart, Guid> { }
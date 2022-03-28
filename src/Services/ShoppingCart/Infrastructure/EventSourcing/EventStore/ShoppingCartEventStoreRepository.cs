using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore;

public class ShoppingCartEventStoreRepository : EventStoreRepository<ShoppingCart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid>, IShoppingCartEventStoreRepository
{
    public ShoppingCartEventStoreRepository(EventStoreDbContext dbContext) 
        : base(dbContext) { }
}
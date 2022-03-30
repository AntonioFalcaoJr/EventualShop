using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class ShoppingCartEventStoreRepository : EventStoreRepository<ShoppingCart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid>, IShoppingCartEventStoreRepository
{
    public ShoppingCartEventStoreRepository(EventStoreDbContext dbContext) 
        : base(dbContext) { }
}
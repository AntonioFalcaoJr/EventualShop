using System;
using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Entities.ShoppingCarts;

namespace Application.EventSourcing.EventStore
{
    public interface IShoppingCartEventStoreRepository : IEventStoreRepository<ShoppingCart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid> { }
}
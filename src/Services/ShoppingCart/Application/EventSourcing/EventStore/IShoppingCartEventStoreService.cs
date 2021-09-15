using System;
using Application.Abstractions.EventSourcing.EventStore;
using Domain.Entities.ShoppingCarts;

namespace Application.EventSourcing.EventStore
{
    public interface IShoppingCartEventStoreService : IEventStoreService<ShoppingCart, Guid> { }
}
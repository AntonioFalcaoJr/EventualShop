using System;
using Application.Abstractions.EventSourcing.EventStore;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore
{
    public interface IShoppingCartEventStoreService : IEventStoreService<Cart, Guid> { }
}
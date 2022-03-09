using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventSourcing.EventStore;

public class ShoppingCartEventStoreRepository : EventStoreRepository<ShoppingCart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid>, IShoppingCartEventStoreRepository
{
    public ShoppingCartEventStoreRepository(DbContext dbContext) 
        : base(dbContext) { }
}
using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Entities.ShoppingCarts;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore
{
    public class AccountEventStoreRepository : EventStoreRepository<ShoppingCart, AccountStoreEvent, AccountSnapshot, Guid>, IAccountEventStoreRepository
    {
        public AccountEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}
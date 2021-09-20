using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates.Accounts;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.Accounts.EventStore.Contexts;

namespace Infrastructure.EventSourcing.Accounts.EventStore
{
    public class AccountEventStoreRepository : EventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid>, IAccountEventStoreRepository
    {
        public AccountEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}
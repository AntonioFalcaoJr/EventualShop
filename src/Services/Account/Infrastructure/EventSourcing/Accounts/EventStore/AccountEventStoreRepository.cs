using System;
using Application.EventSourcing.Accounts.EventStore;
using Application.EventSourcing.Accounts.EventStore.Events;
using Domain.Entities.Accounts;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.Accounts.EventStore.Contexts;

namespace Infrastructure.EventSourcing.Accounts.EventStore
{
    public class AccountEventStoreRepository : EventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid>, IAccountEventStoreRepository
    {
        public AccountEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}
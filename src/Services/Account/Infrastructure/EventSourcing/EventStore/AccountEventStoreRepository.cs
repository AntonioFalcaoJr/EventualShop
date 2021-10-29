using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore;

public class AccountEventStoreRepository : EventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid>, IAccountEventStoreRepository
{
    public AccountEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
}
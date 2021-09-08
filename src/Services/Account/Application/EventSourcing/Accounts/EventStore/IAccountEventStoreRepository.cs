using System;
using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.Accounts.EventStore.Events;
using Domain.Entities.Accounts;

namespace Application.EventSourcing.Accounts.EventStore
{
    public interface IAccountEventStoreRepository : IEventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid> { }
}
using System;
using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Entities.Accounts;

namespace Application.EventSourcing.EventStore
{
    public interface IAccountEventStoreRepository : IEventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid> { }
}
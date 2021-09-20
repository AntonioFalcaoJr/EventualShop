using System;
using Application.Abstractions.EventSourcing.EventStore;
using Domain.Aggregates.Accounts;

namespace Application.EventSourcing.EventStore
{
    public interface IAccountEventStoreService : IEventStoreService<Account, Guid> { }
}
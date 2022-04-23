using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IAccountEventStoreRepository : IEventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid> { }
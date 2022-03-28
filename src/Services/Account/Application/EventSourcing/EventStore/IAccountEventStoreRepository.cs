using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface IAccountEventStoreRepository : IEventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid> { }
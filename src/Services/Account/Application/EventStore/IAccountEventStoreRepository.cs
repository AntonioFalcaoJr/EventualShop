using Application.Abstractions.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;

namespace Application.EventStore;

public interface IAccountEventStoreRepository : IEventStoreRepository<Account, AccountStoreEvent, AccountSnapshot, Guid> { }
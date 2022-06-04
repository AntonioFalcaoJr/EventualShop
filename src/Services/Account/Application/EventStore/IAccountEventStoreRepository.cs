using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IAccountEventStoreRepository : IEventStoreRepository<Account, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }
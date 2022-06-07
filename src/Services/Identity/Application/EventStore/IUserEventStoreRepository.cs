using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IUserEventStoreRepository : IEventStoreRepository<User, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }
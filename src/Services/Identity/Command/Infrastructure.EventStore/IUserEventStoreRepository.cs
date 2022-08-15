using Domain.Abstractions.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;

namespace Infrastructure.EventStore;

public interface IUserEventStoreRepository : IEventStoreRepository<User, UserStoreEvent, UserSnapshot, Guid> { }
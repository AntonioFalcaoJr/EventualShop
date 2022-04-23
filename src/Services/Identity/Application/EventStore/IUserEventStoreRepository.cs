using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IUserEventStoreRepository : IEventStoreRepository<User, UserStoreEvent, UserSnapshot, Guid> { }
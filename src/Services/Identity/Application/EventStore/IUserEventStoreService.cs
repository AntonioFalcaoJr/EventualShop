using Application.Abstractions.EventStore;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IUserEventStoreService : IEventStoreService<User, Guid> { }
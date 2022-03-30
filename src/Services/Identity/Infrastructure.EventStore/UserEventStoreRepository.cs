using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class UserEventStoreRepository : EventStoreRepository<User, UserStoreEvent, UserSnapshot, Guid>, IUserEventStoreRepository
{
    public UserEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
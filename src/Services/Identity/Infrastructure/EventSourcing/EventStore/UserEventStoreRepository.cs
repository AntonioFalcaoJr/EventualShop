using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore;

public class UserEventStoreRepository : EventStoreRepository<User, UserStoreEvent, UserSnapshot, Guid>, IUserEventStoreRepository
{
    public UserEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
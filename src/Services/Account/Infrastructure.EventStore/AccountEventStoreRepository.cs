using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class AccountEventStoreRepository : EventStoreRepository<Account, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IAccountEventStoreRepository
{
    public AccountEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
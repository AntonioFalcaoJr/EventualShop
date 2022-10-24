using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class AccountEventStoreRepository : EventStoreRepository<Account, AccountStoreEvent, AccountSnapshot>, IAccountEventStoreRepository
{
    public AccountEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
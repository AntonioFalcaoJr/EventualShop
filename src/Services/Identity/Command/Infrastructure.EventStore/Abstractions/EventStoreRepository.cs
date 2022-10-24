using Contracts.Abstractions.Messages;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Abstractions;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreDbContext _dbContext;

    public EventStoreRepository(EventStoreDbContext dbContext)
        => _dbContext = dbContext;

    public async Task AppendEventAsync(StoreEvent storeEvent, CancellationToken ct)
    {
        await _dbContext.Set<StoreEvent>().AddAsync(storeEvent, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task AppendSnapshotAsync(Snapshot snapshot, CancellationToken ct)
    {
        await _dbContext.Set<Snapshot>().AddAsync(snapshot, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public Task<List<IEvent>> GetStreamAsync(Guid aggregateId, long version, CancellationToken ct)
        => _dbContext.Set<StoreEvent>()
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(aggregateId))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.DomainEvent)
            .ToListAsync(ct);

    public Task<Snapshot?> GetSnapshotAsync(Guid aggregateId, CancellationToken ct)
        => _dbContext.Set<Snapshot>()
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(ct);
}
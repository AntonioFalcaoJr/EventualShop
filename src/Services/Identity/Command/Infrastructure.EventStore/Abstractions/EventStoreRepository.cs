using Contracts.Abstractions.Messages;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Abstractions;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreDbContext _dbContext;
    private readonly DbSet<Snapshot> _snapshots;
    private readonly DbSet<StoreEvent> _storeEvents;

    public EventStoreRepository(EventStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        _storeEvents = dbContext.Set<StoreEvent>();
        _snapshots = dbContext.Set<Snapshot>();
    }

    public async Task AppendEventsAsync(
        IEnumerable<StoreEvent> events,
        Func<long, CancellationToken, Task> onEventStored,
        CancellationToken ct)
    {
        foreach (var @event in events)
        {
            await AppendEventAsync(@event, ct);
            await onEventStored(@event.Version, ct);
        }
    }

    public async Task AppendEventAsync(StoreEvent storeEvent, CancellationToken ct)
    {
        await _storeEvents.AddAsync(storeEvent, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task AppendSnapshotAsync(Snapshot snapshot, CancellationToken ct)
    {
        await _snapshots.AddAsync(snapshot, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public Task<List<IEvent>> GetStreamAsync(Guid aggregateId, long version, CancellationToken ct)
        => _storeEvents
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(aggregateId))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.DomainEvent)
            .ToListAsync(ct);

    public Task<Snapshot> GetSnapshotAsync(Guid aggregateId, CancellationToken ct)
        => _snapshots
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(ct);
}
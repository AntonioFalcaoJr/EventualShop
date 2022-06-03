using Application.Abstractions.EventStore;
using Application.Abstractions.EventStore.Events;
using Contracts.Abstractions;
using Domain.Abstractions.Aggregates;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Abstractions;

public abstract class EventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> : IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TAggregate, TId>
    where TSnapshot : Snapshot<TAggregate, TId>
    where TId : struct
{
    private readonly EventStoreDbContext _dbContext;
    private readonly DbSet<TSnapshot> _snapshots;
    private readonly DbSet<TStoreEvent> _storeEvents;

    protected EventStoreRepository(EventStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        _storeEvents = dbContext.Set<TStoreEvent>();
        _snapshots = dbContext.Set<TSnapshot>();
    }

    public async Task<long> AppendEventsAsync(TStoreEvent @event, CancellationToken ct)
    {
        await _storeEvents.AddAsync(@event, ct);
        await _dbContext.SaveChangesAsync(ct);
        return @event.Version;
    }

    public async Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken ct)
    {
        await _snapshots.AddAsync(snapshot, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<IEvent>> GetStreamAsync(TId aggregateId, long version, CancellationToken ct)
        => await _storeEvents
            .AsNoTracking()
            .Where(storeEvent => storeEvent.AggregateId.Equals(aggregateId))
            .Where(storeEvent => storeEvent.Version > version)
            .Select(storeEvent => storeEvent.Event)
            .ToListAsync(ct);

    public async Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken ct)
        => await _snapshots
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(ct);
}
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

    public async Task<long> AppendEventToStreamAsync(TStoreEvent storeEvent, CancellationToken cancellationToken)
    {
        await _storeEvents.AddAsync(storeEvent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return storeEvent.Version;
    }

    public async Task AppendSnapshotToStreamAsync(TSnapshot snapshot, CancellationToken cancellationToken)
    {
        await _snapshots.AddAsync(snapshot, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<IEvent>> GetStreamAsync(TId aggregateId, long snapshotVersion, CancellationToken cancellationToken)
        => await _storeEvents
            .AsNoTracking()
            .Where(storeEvent => Equals(storeEvent.AggregateId, aggregateId))
            .Where(storeEvent => storeEvent.Version > snapshotVersion)
            .Select(storeEvent => storeEvent.Event)
            .ToListAsync(cancellationToken);

    public async Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken)
        => await _snapshots
            .AsNoTracking()
            .Where(snapshot => Equals(snapshot.AggregateId, aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(cancellationToken);
}
using Application.Abstractions.EventStore;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Abstractions;

public abstract class EventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> : IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TId, TAggregate>
    where TSnapshot : Snapshot<TId, TAggregate>
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

    public async Task AppendEventsAsync(
        IEnumerable<TStoreEvent> events,
        Func<long, CancellationToken, Task> onEventStored,
        CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await AppendEventAsync(@event, cancellationToken);
            await onEventStored(@event.Version, cancellationToken);
        }
    }

    public async Task AppendEventAsync(TStoreEvent storeEvent, CancellationToken cancellationToken)
    {
        await _storeEvents.AddAsync(storeEvent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken cancellationToken)
    {
        await _snapshots.AddAsync(snapshot, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<IEvent?>> GetStreamAsync(TId aggregateId, long version, CancellationToken cancellationToken)
        => _storeEvents
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(aggregateId))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.DomainEvent)
            .ToListAsync(cancellationToken);

    public Task<TSnapshot?> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken)
        => _snapshots
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(cancellationToken);
}
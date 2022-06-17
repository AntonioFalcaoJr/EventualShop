using Application.Abstractions.EventStore;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Abstractions;

public abstract class EventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId>
    : IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId>
    where TAggregate : IAggregateRoot<TId, TStoreEvent>, new()
    where TStoreEvent : class, IStoreEvent<TId>
    where TSnapshot : class, ISnapshot<TAggregate, TId>, new()
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
        CancellationToken ct)
    {
        foreach (var @event in events)
        {
            await AppendEventAsync(@event, ct);
            await onEventStored(@event.Version, ct);
        }
    }

    public async Task AppendEventAsync(TStoreEvent storeEvent, CancellationToken ct)
    {
        await _storeEvents.AddAsync(storeEvent, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken ct)
    {
        await _snapshots.AddAsync(snapshot, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public Task<List<IEvent>> GetStreamAsync(TId aggregateId, long version, CancellationToken ct)
        => _storeEvents
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(aggregateId))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.DomainEvent)
            .ToListAsync(ct);

    public Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken ct)
        => _snapshots
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(ct);
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.EventStore;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions.Messages.Events;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions.EventSourcing.EventStore;

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

    public async Task<int> AppendEventToStreamAsync(TStoreEvent storeEvent, CancellationToken cancellationToken)
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

    public async Task<IEnumerable<IEvent>> GetStreamAsync(TId aggregateId, int snapshotVersion, CancellationToken cancellationToken)
        => await _storeEvents
            .AsNoTrackingWithIdentityResolution()
            .Where(storeEvent => Equals(storeEvent.AggregateId, aggregateId))
            .Where(storeEvent => storeEvent.Version > snapshotVersion)
            .Select(storeEvent => storeEvent.Event)
            .ToListAsync(cancellationToken);

    public async Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken)
        => await _snapshots
            .AsNoTrackingWithIdentityResolution()
            .Where(snapshot => Equals(snapshot.AggregateId, aggregateId))
            .OrderBy(snapshot => snapshot.AggregateVersion)
            .LastOrDefaultAsync(cancellationToken);
}
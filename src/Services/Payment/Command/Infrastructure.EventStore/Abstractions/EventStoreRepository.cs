using Contracts.Abstractions.Messages;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Abstractions;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreDbContext _dbContext;

    public EventStoreRepository(EventStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AppendEventAsync(StoreEvent storeEvent, CancellationToken cancellationToken)
    {
        await _dbContext.Set<StoreEvent>().AddAsync(storeEvent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AppendSnapshotAsync(Snapshot snapshot, CancellationToken cancellationToken)
    {
        await _dbContext.Set<Snapshot>().AddAsync(snapshot, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<IDomainEvent>> GetStreamAsync(Guid aggregateId, long? version, CancellationToken cancellationToken)
        => _dbContext.Set<StoreEvent>()
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(aggregateId))
            .Where(@event => @event.Version > (version ?? 0))
            .Select(@event => @event.DomainEvent)
            .ToListAsync(cancellationToken);

    public Task<Snapshot?> GetSnapshotAsync(Guid aggregateId, CancellationToken cancellationToken)
        => _dbContext.Set<Snapshot>()
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.AggregateVersion)
            .FirstOrDefaultAsync(cancellationToken);

    public IAsyncEnumerable<Guid> GetAggregateIdsAsync(CancellationToken cancellationToken)
        => _dbContext.Set<StoreEvent>()
            .AsNoTracking()
            .Select(@event => @event.AggregateId)
            .Distinct()
            .AsAsyncEnumerable();
}
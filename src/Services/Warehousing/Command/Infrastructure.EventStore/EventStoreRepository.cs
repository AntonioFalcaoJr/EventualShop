using Contracts.Abstractions.Messages;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore;

public class EventStoreRepository(EventStoreDbContext dbContext) : IEventStoreRepository
{
    public async Task AppendEventAsync(StoreEvent storeEvent, CancellationToken cancellationToken)
    {
        await dbContext.Set<StoreEvent>().AddAsync(storeEvent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AppendSnapshotAsync(Snapshot snapshot, CancellationToken cancellationToken)
    {
        await dbContext.Set<Snapshot>().AddAsync(snapshot, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<IDomainEvent>> GetStreamAsync(Guid aggregateId, ulong? version, CancellationToken cancellationToken)
        => dbContext.Set<StoreEvent>()
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(aggregateId))
            .Where(@event => @event.Version > (version ?? 0))
            .Select(@event => @event.Event)
            .ToListAsync(cancellationToken);

    public Task<Snapshot?> GetSnapshotAsync(Guid aggregateId, CancellationToken cancellationToken)
        => dbContext.Set<Snapshot>()
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(aggregateId))
            .OrderByDescending(snapshot => snapshot.Version)
            .FirstOrDefaultAsync(cancellationToken);

    public IAsyncEnumerable<Guid> StreamAggregatesId()
        => dbContext.Set<StoreEvent>()
            .AsNoTracking()
            .Select(@event => @event.AggregateId)
            .Distinct()
            .AsAsyncEnumerable();
}
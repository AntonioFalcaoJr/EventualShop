using Application.Abstractions.EventStore;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;
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

    public Task AppendEventsAsync(IEnumerable<TStoreEvent> events, Func<TStoreEvent, CancellationToken, Task> onEventStored, CancellationToken cancellationToken)
        => ExecuteInTransactionStrategyAsync(
            operationAsync: async ct =>
            {
                foreach (var @event in events)
                {
                    await _storeEvents.AddAsync(@event, ct);
                    await _dbContext.SaveChangesAsync(ct);
                    await onEventStored(@event, ct);
                }
            },
            cancellationToken: cancellationToken);

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

    private Task ExecuteInTransactionStrategyAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
        => _dbContext.Database.CreateExecutionStrategy().ExecuteAsync(ct => ExecuteInTransactionAsync(operationAsync, ct), cancellationToken);

    private async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        await operationAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Domain.Abstractions.Identities;
using Microsoft.EntityFrameworkCore;
using Version = Domain.ValueObjects.Version;

namespace Infrastructure.EventStore;

public class EventStoreGateway(DbContext dbContext) : IEventStoreGateway
{
    public async Task AppendAsync<TAggregate, TId>(StoreEvent<TAggregate, TId> storeEvent, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
    {
        await dbContext.Set<StoreEvent<TAggregate, TId>>().AddAsync(storeEvent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AppendAsync<TAggregate, TId>(Snapshot<TAggregate, TId> snapshot, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
    {
        await dbContext.Set<Snapshot<TAggregate, TId>>().AddAsync(snapshot, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>(TId id, Version version, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(id))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.Event)
            .ToListAsync(cancellationToken);

    public Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>
        (Expression<Func<StoreEvent<TAggregate, TId>, bool>> predicate, Version version, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Where(predicate)
            .Where(@event => @event.Version > version)
            .Select(@event => @event.Event)
            .ToListAsync(cancellationToken);

    public Task<Snapshot<TAggregate, TId>?> GetSnapshotAsync<TAggregate, TId>(TId id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<Snapshot<TAggregate, TId>>()
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(id))
            .OrderByDescending(snapshot => snapshot.Version)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<Snapshot<TAggregate, TId>?> GetSnapshotAsync<TAggregate, TId>
        (Expression<Func<Snapshot<TAggregate, TId>, bool>> predicate, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<Snapshot<TAggregate, TId>>()
            .AsNoTracking()
            .Where(predicate)
            .OrderByDescending(snapshot => snapshot.Version)
            .FirstOrDefaultAsync(cancellationToken);

    public IAsyncEnumerable<TId> StreamAggregatesId<TAggregate, TId>()
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Select(@event => @event.AggregateId)
            .Distinct()
            .AsAsyncEnumerable();
}
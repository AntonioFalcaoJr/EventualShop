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
    public async Task AppendAsync<TAggregate, TId>(StoreEvent<TAggregate, TId> storeEvent, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
    {
        await dbContext.Set<StoreEvent<TAggregate, TId>>().AddAsync(storeEvent, token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task AppendAsync<TAggregate, TId>(Snapshot<TAggregate, TId> snapshot, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
    {
        await dbContext.Set<Snapshot<TAggregate, TId>>().AddAsync(snapshot, token);
        await dbContext.SaveChangesAsync(token);
    }

    public Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>(TId id, Version version, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(id))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.Event)
            .ToListAsync(token);

    public Task<List<IDomainEvent>> GetStreamAsyncTwo<TAggregate, TId>(
        Expression<Func<StoreEvent<TAggregate, TId>, bool>> predicate, Version version, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Where(predicate)
            .Where(@event => @event.Version > version)
            .Select(@event => @event.Event)
            .ToListAsync(token);

    public Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>
        (Expression<Func<StoreEvent<TAggregate, TId>, bool>> predicate, Version version, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Where(predicate)
            .Where(@event => @event.Version > version)
            .Select(@event => @event.Event)
            .ToListAsync(token);

    public Task<Snapshot<TAggregate, TId>?> GetSnapshotAsync<TAggregate, TId>(TId id, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<Snapshot<TAggregate, TId>>()
            .AsNoTracking()
            .Where(snapshot => snapshot.AggregateId.Equals(id))
            .OrderByDescending(snapshot => snapshot.Version)
            .FirstOrDefaultAsync(token);

    public Task<Snapshot<TAggregate, TId>?> GetSnapshotAsync<TAggregate, TId>
        (Expression<Func<Snapshot<TAggregate, TId>, bool>> predicate, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<Snapshot<TAggregate, TId>>()
            .AsNoTracking()
            .Where(predicate)
            .OrderByDescending(snapshot => snapshot.Version)
            .FirstOrDefaultAsync(token);

    public IAsyncEnumerable<TId> StreamAggregatesId<TAggregate, TId>()
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Select(@event => @event.AggregateId)
            .Distinct()
            .AsAsyncEnumerable();
}
using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Domain.Abstractions.Identities;
using InvalidOperationException = System.InvalidOperationException;
using Version = Domain.ValueObjects.Version;

namespace Application.Services;

public class ApplicationService(
        IEventStoreGateway eventStoreGateway,
        //IOptions<EventStoreOptions> options,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    : IApplicationService
{
    public async Task<TAggregate> LoadAggregateAsync<TAggregate, TId>(TId id, CancellationToken cancellationToken)
        where TAggregate : class, IAggregateRoot<TId>, new()
        where TId : IIdentifier, new()
    {
        var snapshot = await eventStoreGateway.GetSnapshotAsync<TAggregate, TId>(id, cancellationToken);
        var events = await eventStoreGateway.GetStreamAsync<TAggregate, TId>(id, snapshot?.Version ?? Version.Zero, cancellationToken);
        return LoadAggregate(snapshot, events);
    }

    public async Task<TAggregate> LoadAggregateAsync<TAggregate, TId>(Func<TAggregate, bool> predicate, CancellationToken cancellationToken)
        where TAggregate : class, IAggregateRoot<TId>, new()
        where TId : IIdentifier, new()
    {
        var snapshotExpression = BuildExpression<TAggregate, Snapshot<TAggregate, TId>, bool>(predicate);
        var storeEventExpression = BuildExpression<TAggregate, StoreEvent<TAggregate, TId>, bool>(predicate);

        var snapshot = await eventStoreGateway.GetSnapshotAsync(snapshotExpression, cancellationToken);
        var events = await eventStoreGateway.GetStreamAsync(storeEventExpression, snapshot?.Version ?? Version.Zero, cancellationToken);

        if (snapshot is null && events is { Count: 0 }) return new();

        var aggregate = snapshot?.Aggregate ?? new();
        aggregate.LoadFromHistory(events);

        return aggregate is { IsDeleted: false }
            ? aggregate
            : throw new InvalidOperationException($"Aggregate {typeof(TAggregate).Name} is deleted.");
    }

    private static TAggregate LoadAggregate<TAggregate, TId>(Snapshot<TAggregate, TId>? snapshot, List<IDomainEvent> events)
        where TAggregate : class, IAggregateRoot<TId>, new()
        where TId : IIdentifier, new()
    {
        if (snapshot is null && events is { Count: 0 })
            throw new InvalidOperationException($"Aggregate {typeof(TAggregate).Name} not found.");

        var aggregate = snapshot?.Aggregate ?? new TAggregate();
        aggregate.LoadFromHistory(events);

        return aggregate is { IsDeleted: false }
            ? aggregate
            : throw new InvalidOperationException($"Aggregate {typeof(TAggregate).Name} is deleted.");
    }

    private static Expression<Func<TEntity, TResult>> BuildExpression<TInput, TEntity, TResult>(Func<TInput, TResult> func)
    {
        var inputParameter = Expression.Parameter(typeof(TEntity), "entity");
        var convertedParameter = Expression.Convert(inputParameter, typeof(TInput));
        var body = Expression.Invoke(Expression.Constant(func), convertedParameter);
        return Expression.Lambda<Func<TEntity, TResult>>(body, inputParameter);
    }

    public Task AppendEventsAsync<TAggregate, TId>(TAggregate aggregate, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => unitOfWork.ExecuteAsync(
            operationAsync: async ct =>
            {
                while (aggregate.TryDequeueEvent(out var @event))
                {
                    if (@event is null) continue;

                    var storeEvent = StoreEvent<TAggregate, TId>.Create(aggregate, @event);
                    await eventStoreGateway.AppendAsync(storeEvent, ct);

                    if (storeEvent.Version % 5) //options.Value.SnapshotInterval)
                    {
                        var snapshot = Snapshot<TAggregate, TId>.Create(aggregate, storeEvent);
                        await eventStoreGateway.AppendAsync(snapshot, ct);
                    }

                    await eventBusGateway.PublishAsync(@event, ct);
                }
            },
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TId> StreamAggregatesId<TAggregate, TId>()
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => eventStoreGateway.StreamAggregatesId<TAggregate, TId>();

    public Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken)
        => eventBusGateway.PublishAsync(@event, cancellationToken);

    public Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        => eventBusGateway.SchedulePublishAsync(@event, scheduledTime, cancellationToken);
}
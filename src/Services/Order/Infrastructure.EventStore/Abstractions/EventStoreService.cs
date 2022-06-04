using System.Runtime.CompilerServices;
using Application.Abstractions.EventStore;
using Application.Abstractions.Notifications;
using Contracts.Abstractions;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;
using Infrastructure.EventStore.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore.Abstractions;

public abstract class EventStoreService<TAggregate, TStoreEvent, TSnapshot, TId> : IEventStoreService<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TAggregate, TId>, new()
    where TSnapshot : Snapshot<TAggregate, TId>, new()
    where TId : struct
{
    private readonly INotificationContext _notificationContext;
    private readonly EventStoreOptions _options;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> _repository;

    protected EventStoreService(
        IPublishEndpoint publishEndpoint,
        IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> repository,
        INotificationContext notificationContext,
        IOptionsMonitor<EventStoreOptions> optionsMonitor)
    {
        _publishEndpoint = publishEndpoint;
        _notificationContext = notificationContext;
        _options = optionsMonitor.CurrentValue;
        _repository = repository;
    }

    public async Task AppendEventsAsync(TAggregate aggregate, CancellationToken ct)
    {
        if (await aggregate.IsValidAsync is false)
        {
            _notificationContext.AddErrors(aggregate.Errors);
            return;
        }

        var events = GetEventsToStore(aggregate);
        await AppendEventsAsync(aggregate, events, ct);
        await PublishEventsAsync(aggregate.Events, ct);
    }

    public async Task<TAggregate> LoadAggregateAsync(TId aggregateId, CancellationToken ct)
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, ct) ?? new();
        var events = await _repository.GetStreamAsync(aggregateId, snapshot.AggregateVersion, ct);
        snapshot.AggregateState.LoadEvents(events);
        return snapshot.AggregateState;
    }

    private async Task AppendEventsAsync(TAggregate aggregate, IEnumerable<TStoreEvent> events, CancellationToken ct)
    {
        await foreach (var version in AppendEventsAsync(events, ct))
            if (version % _options.SnapshotInterval is 0)
                await AppendSnapshotAsync(aggregate, version, ct);
    }

    private async IAsyncEnumerable<long> AppendEventsAsync(IEnumerable<TStoreEvent> events, [EnumeratorCancellation] CancellationToken ct)
    {
        foreach (var @event in events)
            yield return await _repository.AppendEventsAsync(@event, ct);
    }

    private async Task AppendSnapshotAsync(TAggregate aggregate, long version, CancellationToken ct)
    {
        TSnapshot snapshot = new()
        {
            AggregateId = aggregate.Id,
            AggregateState = aggregate,
            AggregateVersion = version
        };

        await _repository.AppendSnapshotAsync(snapshot, ct);
    }

    private Task PublishEventsAsync(IEnumerable<IEvent> events, CancellationToken ct)
        => Task.WhenAll(events.Select(@event => _publishEndpoint.Publish(@event, @event.GetType(), ct)));

    private static IEnumerable<TStoreEvent> GetEventsToStore(TAggregate aggregate)
        => aggregate.Events.Select(@event
            => new TStoreEvent
            {
                AggregateId = aggregate.Id,
                DomainEvent = @event,
                DomainEventName = @event.GetType().Name
            });
}
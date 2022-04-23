using System.Runtime.CompilerServices;
using Application.Abstractions.EventStore;
using Application.Abstractions.EventStore.Events;
using Application.Abstractions.Notifications;
using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions.Messages.Events;
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

    public async Task AppendEventsToStreamAsync(TAggregate aggregateState, CancellationToken cancellationToken)
    {
        if (await aggregateState.IsValidAsync is false)
        {
            _notificationContext.AddErrors(aggregateState.Errors);
            return;
        }

        var eventsToStore = GetEventsToStore(aggregateState);
        await AppendEventsToStreamWithSnapshotControlAsync(aggregateState, eventsToStore, cancellationToken);
        await PublishEventsAsync(aggregateState.Events, cancellationToken);
    }

    public async Task<TAggregate> LoadAggregateFromStreamAsync(TId aggregateId, CancellationToken cancellationToken)
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, cancellationToken) ?? new();
        var events = await _repository.GetStreamAsync(aggregateId, snapshot.AggregateVersion, cancellationToken);
        snapshot.AggregateState.LoadEvents(events);
        return snapshot.AggregateState;
    }

    private async Task AppendEventsToStreamWithSnapshotControlAsync(TAggregate aggregateState, IEnumerable<TStoreEvent> eventsToStore, CancellationToken cancellationToken)
    {
        await foreach (var version in AppendEventToStreamAsync(eventsToStore, cancellationToken))
            if (version % _options.SnapshotInterval is 0)
                await AppendSnapshotToStreamAsync(aggregateState, version, cancellationToken);
    }

    private async IAsyncEnumerable<long> AppendEventToStreamAsync(IEnumerable<TStoreEvent> storeEvents, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var storeEvent in storeEvents)
            yield return await _repository.AppendEventToStreamAsync(storeEvent, cancellationToken);
    }

    private async Task AppendSnapshotToStreamAsync(TAggregate aggregateState, long aggregateVersion, CancellationToken cancellationToken)
    {
        var snapshot = new TSnapshot
        {
            AggregateId = aggregateState.Id,
            AggregateState = aggregateState,
            AggregateVersion = aggregateVersion
        };

        await _repository.AppendSnapshotToStreamAsync(snapshot, cancellationToken);
    }

    private Task PublishEventsAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
        => Task.WhenAll(events.Select(@event => _publishEndpoint.Publish(@event, @event.GetType(), cancellationToken)));

    private static IEnumerable<TStoreEvent> GetEventsToStore(TAggregate aggregateState)
        => aggregateState.Events.Select(@event
            => new TStoreEvent
            {
                AggregateId = aggregateState.Id,
                Event = @event,
                EventName = @event.GetType().Name
            });
}
using System.Runtime.CompilerServices;
using Application.Abstractions.EventSourcing.EventStore;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions.Events;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Abstractions.EventSourcing.EventStore;

public abstract class EventStoreService<TAggregateState, TStoreEvent, TSnapshot, TId> : IEventStoreService<TAggregateState, TId>
    where TAggregateState : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TAggregateState, TId>, new()
    where TSnapshot : Snapshot<TAggregateState, TId>, new()
    where TId : struct
{
    private readonly IBus _bus;
    private readonly ILogger _logger;
    private readonly EventStoreOptions _options;
    private readonly IEventStoreRepository<TAggregateState, TStoreEvent, TSnapshot, TId> _repository;

    protected EventStoreService(
        ILogger logger,
        IOptionsMonitor<EventStoreOptions> optionsMonitor,
        IEventStoreRepository<TAggregateState, TStoreEvent, TSnapshot, TId> repository,
        IBus bus)
    {
        _logger = logger;
        _repository = repository;
        _bus = bus;
        _options = optionsMonitor.CurrentValue;
    }

    public async Task AppendEventsToStreamAsync(TAggregateState aggregateState, CancellationToken cancellationToken)
    {
        if (aggregateState.IsValid is false)
        {
            _logger.LogError("Business error: {Errors}", aggregateState.Errors);
            // TODO - Add Notification
            return;
        }

        var eventsToStore = GetEventsToStore(aggregateState);
        await AppendEventsToStreamWithSnapshotControlAsync(aggregateState, eventsToStore, cancellationToken);
        await PublishEventsAsync(aggregateState.Events, cancellationToken);
    }

    public async Task<TAggregateState> LoadAggregateFromStreamAsync(TId aggregateId, CancellationToken cancellationToken)
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, cancellationToken) ?? new();
        var events = await _repository.GetStreamAsync(aggregateId, snapshot.AggregateVersion, cancellationToken);
        snapshot.AggregateState.LoadEvents(events);
        return snapshot.AggregateState;
    }

    private async Task AppendEventsToStreamWithSnapshotControlAsync(TAggregateState aggregateState, IEnumerable<TStoreEvent> eventsToStore, CancellationToken cancellationToken)
    {
        await foreach (var version in AppendEventToStreamAsync(eventsToStore, cancellationToken))
            if (version % _options.SnapshotInterval is 0)
                await AppendSnapshotToStreamAsync(aggregateState, version, cancellationToken);
    }

    private async IAsyncEnumerable<int> AppendEventToStreamAsync(IEnumerable<TStoreEvent> storeEvents, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var storeEvent in storeEvents)
            yield return await _repository.AppendEventToStreamAsync(storeEvent, cancellationToken);
    }

    private async Task AppendSnapshotToStreamAsync(TAggregateState aggregateState, int aggregateVersion, CancellationToken cancellationToken)
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
        => Task.WhenAll(events.Select(@event => _bus.Publish(@event, @event.GetType(), cancellationToken)));

    private static IEnumerable<TStoreEvent> GetEventsToStore(TAggregateState aggregateState)
        => aggregateState.Events.Select(@event
            => new TStoreEvent
            {
                AggregateId = aggregateState.Id,
                Event = @event,
                EventName = @event.GetType().Name
            });
}
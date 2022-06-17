using Application.Abstractions.EventStore;
using Application.Abstractions.Notifications;
using Contracts.Abstractions.Messages;
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

    public async Task AppendEventsAsync(TAggregate aggregate, CancellationToken cancellationToken)
    {
        if (await aggregate.IsValidAsync is false)
        {
            _notificationContext.AddErrors(aggregate.Errors);
            return;
        }

        async Task OnEventStored(TStoreEvent storeEvent, CancellationToken ct)
        {
            await AppendSnapshotAsync(aggregate, storeEvent.Version, ct);
            await PublishEventAsync(storeEvent.DomainEvent, ct);
        }

        await _repository.AppendEventsAsync(GetEventsToStore(aggregate), OnEventStored, cancellationToken);
    }

    public async Task<TAggregate> LoadAggregateAsync(TId aggregateId, CancellationToken ct)
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, ct) ?? new();
        var events = await _repository.GetStreamAsync(aggregateId, snapshot.AggregateVersion, ct);
        snapshot.AggregateState.LoadEvents(events);
        return snapshot.AggregateState;
    }

    private Task PublishEventAsync(IEvent @event, CancellationToken ct)
        => _publishEndpoint.Publish(@event, @event.GetType(), ct);

    private async Task AppendSnapshotAsync(TAggregate aggregate, long version, CancellationToken ct)
    {
        if (version % _options.SnapshotInterval is not 0) return;

        TSnapshot snapshot = new()
        {
            AggregateId = aggregate.Id,
            AggregateState = aggregate,
            AggregateVersion = version
        };

        await _repository.AppendSnapshotAsync(snapshot, ct);
    }

    private static IEnumerable<TStoreEvent> GetEventsToStore(TAggregate aggregate)
        => aggregate.Events.Select(@event
            => new TStoreEvent
            {
                AggregateId = aggregate.Id,
                DomainEvent = @event,
                DomainEventName = @event.GetType().Name
            });
}
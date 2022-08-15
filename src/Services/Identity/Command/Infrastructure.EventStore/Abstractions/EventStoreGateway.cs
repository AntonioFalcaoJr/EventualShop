using Application.Abstractions;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore.Abstractions;

public abstract class EventStoreGateway<TStoreEvent, TSnapshot, TId> : IEventStoreGateway
    where TStoreEvent : StoreEvent<TId, IAggregateRoot<TId>>, new()
    where TSnapshot : Snapshot<TId, IAggregateRoot<TId>>, new()
    where TId : struct
{
    private readonly EventStoreOptions _options;
    private readonly IEventStoreRepository<IAggregateRoot<TId>, TStoreEvent, TSnapshot, TId> _repository;

    protected EventStoreGateway(
        IEventStoreRepository<IAggregateRoot<TId>, TStoreEvent, TSnapshot, TId> repository,
        IOptionsMonitor<EventStoreOptions> optionsMonitor)
    {
        _options = optionsMonitor.CurrentValue;
        _repository = repository;
    }

    public async Task AppendSnapshotAsync(IAggregateRoot<TId> aggregate, long version, CancellationToken cancellationToken)
    {
        if (version % _options.SnapshotInterval is not 0) return;

        TSnapshot snapshot = new()
        {
            AggregateId = aggregate.Id,
            AggregateState = aggregate,
            AggregateVersion = version
        };

        await _repository.AppendSnapshotAsync(snapshot, cancellationToken);
    }

    private static IEnumerable<TStoreEvent> ToStoreEvents(IAggregateRoot<TId> aggregate)
        => aggregate.Events.Select(@event
            => new TStoreEvent
            {
                Version = aggregate.Version,
                AggregateId = aggregate.Id,
                DomainEvent = @event,
                DomainEventName = @event.GetType().Name
            });

    public Task AppendAsync<TId1>(IAggregateRoot<TId> aggregate, CancellationToken cancellationToken)
        => _repository.AppendEventsAsync(
            events: ToStoreEvents(aggregate),
            onEventStored: (version, ct) => AppendSnapshotAsync(aggregate, version, ct),
            cancellationToken: cancellationToken);

    public async Task<IAggregateRoot<TId>> LoadAsync(TId aggregateId, CancellationToken ct)
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, ct) ?? new();
        var events = await _repository.GetStreamAsync(aggregateId, snapshot.AggregateVersion, ct);
        snapshot.AggregateState.Load(events);
        return snapshot.AggregateState;
    }
}
using Application.Abstractions;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore.Abstractions;

public class EventStoreGateway : IEventStoreGateway
{
    private readonly EventStoreOptions _options;
    private readonly IEventStoreRepository _repository;

    public EventStoreGateway(
        IEventStoreRepository repository,
        IOptionsMonitor<EventStoreOptions> optionsMonitor)
    {
        _options = optionsMonitor.CurrentValue;
        _repository = repository;
    }

    private async Task AppendSnapshotAsync(IAggregateRoot aggregate, long version, CancellationToken cancellationToken)
    {
        if (version % _options.SnapshotInterval is not 0) return;
        Snapshot snapshot = new(aggregate.Id, aggregate.GetType().Name, aggregate, version);
        await _repository.AppendSnapshotAsync(snapshot, cancellationToken);
    }

    private static IEnumerable<StoreEvent> ToStoreEvents(IAggregateRoot aggregate)
        => aggregate.Events.Select(@event 
            => new StoreEvent(aggregate.Version, aggregate.Id, aggregate.GetType().Name, @event, @event.GetType().Name));

    public Task AppendAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _repository.AppendEventsAsync(
            events: ToStoreEvents(aggregate),
            onEventStored: (version, ct) => AppendSnapshotAsync(aggregate, version, ct),
            cancellationToken: cancellationToken);

    public async Task<IAggregateRoot> LoadAsync<TAggregate>(Guid aggregateId, CancellationToken ct)
        where TAggregate : IAggregateRoot, new()
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, ct);
        var events = await _repository.GetStreamAsync(aggregateId, snapshot?.AggregateVersion ?? default, ct);
        return snapshot?.Aggregate?.Load(events) ?? new TAggregate().Load(events);
    }
}
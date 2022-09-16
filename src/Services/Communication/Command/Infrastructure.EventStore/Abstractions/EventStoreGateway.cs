using Application.Abstractions.Gateways;
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
        IOptionsSnapshot<EventStoreOptions> options)
    {
        _options = options.Value;
        _repository = repository;
    }

    private async Task AppendSnapshotAsync(IAggregateRoot aggregate, long version, CancellationToken cancellationToken)
    {
        if (version % _options.SnapshotInterval is not 0) return;
        Snapshot snapshot = new(version, aggregate);
        await _repository.AppendSnapshotAsync(snapshot, cancellationToken);
    }

    public Task AppendAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _repository.AppendEventsAsync(
            events: aggregate.Events.Select(@event => new StoreEvent(aggregate, @event)),
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
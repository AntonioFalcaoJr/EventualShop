using Application.Abstractions;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Infrastructure.EventStore.Abstractions;

public class EventStoreGateway : IEventStoreGateway
{
    private readonly EventStoreOptions _options;
    private readonly IEventStoreRepository _repository;

    public EventStoreGateway(IEventStoreRepository repository, IOptionsSnapshot<EventStoreOptions> options)
    {
        _options = options.Value;
        _repository = repository;
    }

    public async Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        foreach (var @event in aggregate.Events.Select(@event => new StoreEvent(aggregate, @event)))
        {
            await _repository.AppendEventAsync(@event, cancellationToken);
            await AppendSnapshotAsync(aggregate, @event.Version, cancellationToken);
        }
    }

    public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, cancellationToken);
        var events = await _repository.GetStreamAsync(aggregateId, snapshot?.AggregateVersion ?? default, cancellationToken);
        return (TAggregate)(snapshot?.Aggregate.Load(events) ?? new TAggregate().Load(events));
    }

    public IAsyncEnumerable<Guid> GetAggregateIdsAsync(CancellationToken cancellationToken)
        => _repository.GetAggregateIdsAsync(cancellationToken);

    private async Task AppendSnapshotAsync(IAggregateRoot aggregate, long version, CancellationToken cancellationToken)
    {
        if (version % _options.SnapshotInterval is not 0) return;
        Snapshot snapshot = new(version, aggregate);
        await _repository.AppendSnapshotAsync(snapshot, cancellationToken);
    }
}
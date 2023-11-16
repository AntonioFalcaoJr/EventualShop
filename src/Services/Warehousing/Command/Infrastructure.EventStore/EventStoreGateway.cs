using Application.Abstractions.Gateways;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.DependencyInjection.Options;
using Infrastructure.EventStore.Exceptions;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore;

public class EventStoreGateway(IEventStoreRepository repository, IOptions<EventStoreOptions> options)
    : IEventStoreGateway
{
    private readonly EventStoreOptions _options = options.Value;

    public async Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        foreach (var @event in aggregate.UncommittedEvents.Select(@event => StoreEvent.Create(aggregate, @event)))
        {
            await repository.AppendEventAsync(@event, cancellationToken);

            if (@event.Version % _options.SnapshotInterval is 0)
                await repository.AppendSnapshotAsync(Snapshot.Create(aggregate, @event), cancellationToken);
        }
    }

    public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
    {
        var snapshot = await repository.GetSnapshotAsync(aggregateId, cancellationToken);
        var events = await repository.GetStreamAsync(aggregateId, snapshot?.Version, cancellationToken);

        if (snapshot is null && events is { Count: 0 })
            throw new AggregateNotFoundException(aggregateId, typeof(TAggregate));

        var aggregate = snapshot?.Aggregate ?? new TAggregate();

        aggregate.LoadFromHistory(events);

        return (TAggregate)aggregate;
    }

    public IAsyncEnumerable<Guid> StreamAggregatesId()
        => repository.StreamAggregatesId();
}
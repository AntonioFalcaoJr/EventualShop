using System.Runtime.CompilerServices;
using Application.Abstractions.Gateways;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.DependencyInjection.Options;
using Infrastructure.EventStore.Exceptions;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore;

public class EventStoreGateway : IEventStoreGateway
{
    private readonly EventStoreOptions _options;
    private readonly IEventStoreRepository _repository;

    public EventStoreGateway(
        IEventStoreRepository repository,
        IOptions<EventStoreOptions> options)
    {
        _options = options.Value;
        _repository = repository;
    }

    public async Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        foreach (var @event in aggregate.UncommittedEvents.Select(@event => StoreEvent.Create(aggregate, @event)))
        {
            await _repository.AppendEventAsync(@event, cancellationToken);

            if (@event.Version % _options.SnapshotInterval is 0)
                await _repository.AppendSnapshotAsync(Snapshot.Create(aggregate, @event), cancellationToken);
        }
    }

    public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
    {
        var snapshot = await _repository.GetSnapshotAsync(aggregateId, cancellationToken);
        var events = await _repository.GetStreamAsync(aggregateId, snapshot?.Version, cancellationToken);

        if (snapshot is null && events is not { Count: > 0 })
            throw new AggregateNotFoundException(aggregateId, typeof(TAggregate));

        var aggregate = snapshot?.Aggregate ?? new TAggregate();

        return (TAggregate)aggregate.Load(events);
    }

    public ConfiguredCancelableAsyncEnumerable<Guid> StreamAggregatesId(CancellationToken cancellationToken)
        => _repository.StreamAggregatesId(cancellationToken);

    public Task ExecuteTransactionAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
        => _repository.ExecuteTransactionAsync(operationAsync, cancellationToken);
}
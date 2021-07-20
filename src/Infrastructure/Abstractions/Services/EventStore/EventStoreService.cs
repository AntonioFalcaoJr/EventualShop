using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.Abstractions.EventSourcing.EventStore.Repositories;
using Infrastructure.DependencyInjection.Options;
using MassTransit.Mediator;
using Microsoft.Extensions.Options;

namespace Infrastructure.Abstractions.Services.EventStore
{
    public abstract class EventStoreService<TAggregate, TStoreEvent, TSnapshot, TId> : IEventStoreService<TAggregate, TId>
        where TAggregate : IAggregateRoot<TId>, new()
        where TStoreEvent : StoreEvent<TAggregate, TId>, new()
        where TSnapshot : Snapshot<TAggregate, TId>, new()
        where TId : struct
    {
        private readonly IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> _repository;
        private readonly IMediator _mediator;
        private readonly EventStoreOptions _options;

        protected EventStoreService(IOptions<EventStoreOptions> options, IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, TId> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _options = options.Value;
        }

        public async Task AppendEventsToStreamAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            var events = GetEventsToStore(aggregate);

            await foreach (var version in AppendEventToStreamAsync(events, cancellationToken))
            {
                if (version % _options.SnapshotInterval is not 0) continue;
                await AppendSnapshotToStreamAsync(aggregate, version, cancellationToken);
            }

            await PublishEventsAsync(aggregate.Events, cancellationToken);
        }

        private async IAsyncEnumerable<int> AppendEventToStreamAsync(IEnumerable<TStoreEvent> events, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            foreach (var @event in events)
                yield return await _repository.AppendEventToStreamAsync(@event, cancellationToken);
        }

        private async Task AppendSnapshotToStreamAsync(TAggregate aggregate, int latest, CancellationToken cancellationToken)
        {
            var snapshot = new TSnapshot
            {
                AggregateId = aggregate.Id,
                Aggregate = aggregate,
                Version = latest
            };

            await _repository.AppendSnapshotToStreamAsync(snapshot, cancellationToken);
        }

        public async Task<TAggregate> LoadAggregateFromStreamAsync(TId aggregateId, CancellationToken cancellationToken)
        {
            var snapshot = await _repository.GetSnapshotAsync(aggregateId, cancellationToken) ?? new();
            var events = await _repository.GetStreamAsync(aggregateId, snapshot.Version, cancellationToken);
            snapshot.Aggregate.Load(events);
            return snapshot.Aggregate;
        }

        private async Task PublishEventsAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken)
        {
            foreach (var @event in events)
                await _mediator.Publish(@event, cancellationToken);
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
}
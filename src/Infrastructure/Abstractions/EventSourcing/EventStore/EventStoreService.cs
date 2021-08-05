using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Models;
using Application.Abstractions.EventSourcing.Repositories;
using Application.Abstractions.EventSourcing.Services;
using Application.DependencyInjection.Options;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Options;

namespace Infrastructure.Abstractions.EventSourcing.EventStore
{
    public abstract class EventStoreService<TAggregateState, TStoreEvent, TSnapshot, TId> : IEventStoreService<TAggregateState, TId>
        where TAggregateState : IAggregateRoot<TId>, new()
        where TStoreEvent : StoreEvent<TAggregateState, TId>, new()
        where TSnapshot : Snapshot<TAggregateState, TId>, new()
        where TId : struct
    {
        private readonly IEventStoreRepository<TAggregateState, TStoreEvent, TSnapshot, TId> _repository;
        private readonly IMediator _mediator;
        private readonly EventStoreOptions _options;

        protected EventStoreService(IOptions<EventStoreOptions> options, IEventStoreRepository<TAggregateState, TStoreEvent, TSnapshot, TId> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _options = options.Value;
        }

        public async Task AppendEventsToStreamAsync(TAggregateState aggregateState, CancellationToken cancellationToken)
        {
            var eventsToStore = GetEventsToStore(aggregateState);

            await foreach (var version in AppendEventToStreamAsync(eventsToStore, cancellationToken))
                if (version % _options.SnapshotInterval is 0)
                    await AppendSnapshotToStreamAsync(aggregateState, version, cancellationToken);

            await PublishEventsAsync(aggregateState.DomainEvents, cancellationToken);
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

        public async Task<TAggregateState> LoadAggregateFromStreamAsync(TId aggregateId, CancellationToken cancellationToken)
        {
            var snapshot = await _repository.GetSnapshotAsync(aggregateId, cancellationToken) ?? new TSnapshot();
            var events = await _repository.GetStreamAsync(aggregateId, snapshot.AggregateVersion, cancellationToken);
            snapshot.AggregateState.LoadEvents(events);
            return snapshot.AggregateState;
        }

        private Task PublishEventsAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken)
            => _mediator.PublishBatch(events, cancellationToken);

        private static IEnumerable<TStoreEvent> GetEventsToStore(TAggregateState aggregateState)
            => aggregateState.DomainEvents.Select(domainEvent
                => new TStoreEvent
                {
                    AggregateId = aggregateState.Id,
                    DomainEvent = domainEvent,
                    DomainEventName = domainEvent.GetType().Name
                });
    }
}
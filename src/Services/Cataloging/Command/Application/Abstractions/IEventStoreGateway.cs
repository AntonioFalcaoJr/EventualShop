using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Domain.Abstractions.Identities;
using Version = Domain.ValueObjects.Version;

namespace Application.Abstractions;

public interface IEventStoreGateway
{
    Task AppendAsync<TAggregate, TId>(StoreEvent<TAggregate, TId> storeEvent, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task AppendAsync<TAggregate, TId>(Snapshot<TAggregate, TId> snapshot, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>(TId id, Version version, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task<Snapshot<TAggregate, TId>?> GetSnapshotAsync<TAggregate, TId>(TId id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    IAsyncEnumerable<TId> StreamAggregatesId<TAggregate, TId>()
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();
}
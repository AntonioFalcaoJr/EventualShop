using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Identities;
using Version = Domain.ValueObjects.Version;

namespace Domain.Abstractions.EventStore;

public record StoreEvent<TAggregate, TId>(
    TId AggregateId, 
    string EventType, 
    IDomainEvent Event, 
    Version Version, 
    DateTimeOffset Timestamp, 
    string? ReferenceId)
    where TAggregate : IAggregateRoot<TId>
    where TId : IIdentifier, new()
{
    public static StoreEvent<TAggregate, TId> Create(TAggregate aggregate, IDomainEvent @event, string? referenceId = null)
        => new(aggregate.Id, @event.GetType().Name, @event, (Version)@event.Version, @event.Timestamp, referenceId);
}
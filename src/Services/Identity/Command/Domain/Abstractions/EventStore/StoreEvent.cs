using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(long Version, Guid AggregateId, string AggregateName, IEvent DomainEvent, string DomainEventName);
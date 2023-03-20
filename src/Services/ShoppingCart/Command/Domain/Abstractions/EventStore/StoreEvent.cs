﻿using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(IIdentifier AggregateId, string AggregateType, string EventType, IDomainEvent Event, long Version, DateTimeOffset Timestamp)
{
    public static StoreEvent Create(IAggregateRoot<IIdentifier> aggregate, IDomainEvent @event)
        => new(aggregate.Id, aggregate.GetType().Name, @event.GetType().Name, @event, @event.Version, @event.Timestamp);
}
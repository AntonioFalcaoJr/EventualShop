using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Identities;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId> : IEntity<TId>
    where TId : IIdentifier, new()
{
    void LoadFromStream(List<IDomainEvent> events);
    bool TryDequeueEvent(out IDomainEvent @event);
}
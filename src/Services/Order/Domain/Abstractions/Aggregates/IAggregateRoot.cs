using Domain.Abstractions.Entities;
using ECommerce.Abstractions.Events;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId> : IEntity<TId>
    where TId : struct
{
    IEnumerable<IEvent> Events { get; }
    void LoadEvents(IEnumerable<IEvent> events);
}
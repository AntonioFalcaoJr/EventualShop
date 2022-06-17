using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.StoreEvents;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId, out TStoreEvent> : IEntity<TId>
    where TId : struct
    where TStoreEvent : class, IStoreEvent<TId>
{
    IEnumerable<IEvent> Events { get; }
    IEnumerable<TStoreEvent> StoreEvents { get; }
    void LoadEvents(List<IEvent> events);
}
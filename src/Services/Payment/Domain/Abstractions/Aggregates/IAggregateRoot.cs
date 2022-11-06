using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId> : IEntity<TId>
    where TId : struct
{
    long Version { get; }
    IEnumerable<IEvent?> Events { get; }
    void LoadEvents(List<IEvent?> events);
}
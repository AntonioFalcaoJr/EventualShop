using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot : IEntity
{
    long Version { get; }
    IEnumerable<IEvent> Events { get; }
    void LoadEvents(List<IEvent> events);
}
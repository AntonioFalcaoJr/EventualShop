using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId> : IEntity<TId>
    where TId : struct
{
    long Version { get; }
    IEnumerable<IEvent> UncommittedEvents { get; }
    public void LoadEvents(IEnumerable<IEvent> events);
    void Handle(ICommand command);
}
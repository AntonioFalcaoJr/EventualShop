using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot : IEntity
{
    long Version { get; }
    IEnumerable<IEvent> UncommittedEvents { get; }
    public void LoadEvents(IEnumerable<IEvent> events);
    void Handle(ICommand? command);
}
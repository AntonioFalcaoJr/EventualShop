using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot : IEntity
{
    IEnumerable<(long version, IEvent @event)> UncommittedEvents { get; }
    IAggregateRoot Load(IEnumerable<IEvent> events);
    void Handle(ICommand command);
}
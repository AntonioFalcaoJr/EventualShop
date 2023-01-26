using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot : IEntity
{
    IEnumerable<IVersionedEvent> UncommittedEvents { get; }
    IAggregateRoot Load(IEnumerable<IVersionedEvent> events);
    void Handle(ICommand command);
}
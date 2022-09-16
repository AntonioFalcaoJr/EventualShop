using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot : IEntity
{
    IEnumerable<(long version, IEvent @event)> Events { get; }
    IAggregateRoot Load(List<IEvent> events);
    void Handle(ICommandWithId command);
}
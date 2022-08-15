using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId> : IEntity<TId>
    where TId : struct
{
    long Version { get; }
    IEnumerable<IEvent> Events { get; }
    void Load(List<IEvent> events);
    void Handle(ICommand command);
}
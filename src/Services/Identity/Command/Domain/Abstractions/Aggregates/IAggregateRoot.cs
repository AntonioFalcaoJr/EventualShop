using System.Text.Json.Serialization;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using Domain.Aggregates;

namespace Domain.Abstractions.Aggregates;

[JsonDerivedType(typeof(User), nameof(User))]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
public interface IAggregateRoot : IEntity
{
    IEnumerable<(long version, IEvent @event)> Events { get; }
    IAggregateRoot Load(List<IEvent> events);
    void Handle(ICommand command);
}
using System.Text.Json.Serialization;
using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;
using Domain.Aggregates;

namespace Domain.Abstractions.Aggregates;

[JsonDerivedType(typeof(Account), nameof(Account))]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
public interface IAggregateRoot : IEntity
{
    long Version { get; }
    IEnumerable<IEvent> Events { get; }
    void LoadEvents(List<IEvent> events);
}
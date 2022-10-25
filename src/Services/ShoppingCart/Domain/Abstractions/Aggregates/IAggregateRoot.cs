using System.Text.Json.Serialization;
using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;
using Domain.Aggregates;

namespace Domain.Abstractions.Aggregates;

[JsonDerivedType(typeof(ShoppingCart), nameof(ShoppingCart))]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
public interface IAggregateRoot : IEntity
{
    long Version { get; }
    IEnumerable<IEvent> UncommittedEvents { get; }
    public void LoadEvents(IEnumerable<IEvent> events);
    void Handle(ICommand? command);
}
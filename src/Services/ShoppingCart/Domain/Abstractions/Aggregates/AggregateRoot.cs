using System.Collections.Generic;
using Domain.Abstractions.Entities;
using ECommerce.Abstractions.Events;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    where TId : struct
{
    [JsonIgnore]
    private readonly List<IEvent> _events = new();

    [JsonIgnore]
    public IEnumerable<IEvent> Events
        => _events;

    public void LoadEvents(IEnumerable<IEvent> events)
    {
        foreach (var @event in events)
            ApplyEvent((dynamic)@event);
    }

    private void AddEvent(IEvent @event)
        => _events.Add(@event);

    protected abstract void ApplyEvent(IEvent @event);

    protected void RaiseEvent(IEvent @event)
    {
        ApplyEvent((dynamic)@event);
        if (IsValid is false) return;
        AddEvent(@event);
    }
}
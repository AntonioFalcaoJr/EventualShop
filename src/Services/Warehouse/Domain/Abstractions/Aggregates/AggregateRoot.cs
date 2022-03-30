using Domain.Abstractions.Entities;
using ECommerce.Abstractions.Messages.Events;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId, TValidator> : Entity<TId, TValidator>, IAggregateRoot<TId>
    where TId : struct
    where TValidator : IValidator, new()
{
    [JsonIgnore]
    private readonly List<IEvent> _events = new();

    [JsonIgnore]
    public IEnumerable<IEvent> Events
        => _events;

    public void LoadEvents(IEnumerable<IEvent> events)
    {
        foreach (var @event in events)
            ApplyEvent(@event);
    }

    private void AddEvent(IEvent @event)
        => _events.Add(@event);

    protected abstract void ApplyEvent(IEvent @event);

    protected void RaiseEvent(IEvent @event)
    {
        ApplyEvent(@event);
        if (IsValid) AddEvent(@event);
    }
}
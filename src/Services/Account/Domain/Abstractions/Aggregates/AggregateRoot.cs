using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId, TValidator> : Entity<TId, TValidator>, IAggregateRoot<TId>
    where TValidator : IValidator, new()
    where TId : struct
{
    [JsonIgnore]
    private readonly List<IEvent> _events = new();

    [JsonIgnore]
    public IEnumerable<IEvent> Events
        => _events;

    public void LoadEvents(List<IEvent> events)
        => events.ForEach(ApplyEvent);

    private void AddEvent(IEvent @event)
        => _events.Add(@event);

    protected abstract void ApplyEvent(IEvent @event);

    protected void RaiseEvent(IEvent @event)
    {
        ApplyEvent(@event);
        if (IsValid) AddEvent(@event);
    }
}
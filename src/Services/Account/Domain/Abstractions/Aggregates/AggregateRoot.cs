using Domain.Abstractions.Entities;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.StoreEvents;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId, TValidator, TStoreEvent>
    : Entity<TId, TValidator>, IAggregateRoot<TId, TStoreEvent>
    where TId : struct
    where TValidator : IValidator, new()
    where TStoreEvent : class, IStoreEvent<TId>, new()
{
    [JsonIgnore]
    private readonly List<IEvent> _events = new();

    [JsonIgnore]
    public IEnumerable<IEvent> Events
        => _events;

    [JsonIgnore]
    public IEnumerable<TStoreEvent> StoreEvents
        => _events.Select(@event => new TStoreEvent
        {
            AggregateId = Id,
            DomainEvent = @event,
            DomainEventName = @event.GetType().Name
        });

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
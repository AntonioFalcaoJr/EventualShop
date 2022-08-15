using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId, TValidator> : Entity<TId, TValidator>, IAggregateRoot<TId>
    where TValidator : IValidator, new()
    where TId : struct
{
    [JsonIgnore]
    private readonly List<IEvent> _events = new();

    public long Version { get; private set; }

    [JsonIgnore]
    public IEnumerable<IEvent> Events
        => _events;

    public void Load(List<IEvent> events)
    {
        events.ForEach(@event =>
        {
            Apply(@event);
            Version += 1;
        });
    }

    public abstract void Handle(ICommand command);

    protected void Raise(IEvent @event)
    {
        Apply(@event);

        if (IsValid)
        {
            _events.Add(@event);
            Version += 1;
        }
    }

    protected abstract void Apply(IEvent @event);
}
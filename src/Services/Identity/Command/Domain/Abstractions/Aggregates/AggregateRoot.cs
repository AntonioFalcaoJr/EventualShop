using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TValidator> : Entity<TValidator>, IAggregateRoot
    where TValidator : IValidator, new()
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

    public abstract void Handle(ICommandWithId command);

    protected void Raise(IEvent @event)
    {
        Apply(@event);
        Validate();
        _events.Add(@event);
        Version += 1;
    }

    protected abstract void Apply(IEvent @event);
}
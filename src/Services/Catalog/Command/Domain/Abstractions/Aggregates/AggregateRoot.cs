using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TValidator> : Entity<TValidator>, IAggregateRoot
    where TValidator : IValidator, new()
{
    [JsonIgnore]
    private readonly List<(long version, IEvent @event)> _events = new();

    public long Version { get; private set; }

    [JsonIgnore]
    public IEnumerable<(long version, IEvent @event)> UncommittedEvents
        => _events;

    public IAggregateRoot Load(IEnumerable<IEvent> events)
    {
        foreach (var @event in events)
        {
            Apply(@event);
            Version += 1;
        }

        return this;
    }

    public abstract void Handle(ICommand command);

    protected void RaiseEvent(IEvent @event)
    {
        Apply(@event);
        Validate();
        Version += 1;
        _events.Add((Version, @event));
    }

    protected abstract void Apply(IEvent @event);
}
using System.Text.Json.Serialization;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using FluentValidation;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TValidator> : Entity<TValidator>, IAggregateRoot
    where TValidator : IValidator, new()
{
    [JsonIgnore]
    private readonly List<(long version, IEvent @event)> _events = new();

    private long Version { get; set; }

    [JsonIgnore]
    public IEnumerable<(long version, IEvent @event)> Events
        => _events;

    public IAggregateRoot Load(List<IEvent> events)
    {
        events.ForEach(@event =>
        {
            Apply(@event);
            Version += 1;
        });

        return this;
    }

    public abstract void Handle(ICommand command);

    protected void RaiseEvent(IEvent @event)
    {
        Apply(@event);
        Validate();
        _events.Add((Version += 1, @event));
    }

    protected abstract void Apply(IEvent @event);
}
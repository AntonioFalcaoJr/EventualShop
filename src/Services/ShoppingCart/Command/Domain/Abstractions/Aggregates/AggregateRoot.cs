using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TValidator> : Entity<TValidator>, IAggregateRoot
    where TValidator : IValidator, new()
{
    private readonly List<IVersionedEvent> _events = new();

    public long Version { get; protected set; }
    protected long ExpectedVersion => Version++;

    [JsonIgnore]
    public IEnumerable<IVersionedEvent> UncommittedEvents
        => _events;

    public IAggregateRoot Load(IEnumerable<IVersionedEvent> events)
    {
        foreach (var @event in events)
        {
            Apply(@event);
            Version = @event.Version;
        }

        return this;
    }

    public abstract void Handle(ICommand command);

    protected void RaiseEvent(IVersionedEvent @event)
    {
        Apply(@event);
        Version = @event.Version;
        Validate();
        _events.Add(@event);
    }

    protected abstract void Apply(IVersionedEvent @event);
}
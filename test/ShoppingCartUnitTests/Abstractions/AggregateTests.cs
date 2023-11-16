using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Identities;
using FluentAssertions;

namespace ShoppingCartUnitTests.Abstractions;

public abstract class AggregateTests<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TId : IIdentifier, new()
{
    protected TAggregate AggregateRoot = new();
    private Action<TAggregate>? _command;

    protected AggregateTests<TAggregate, TId> Given(params IDomainEvent[] events)
    {
        AggregateRoot.LoadFromHistory(events);
        return this;
    }

    public AggregateTests<TAggregate, TId> When(TAggregate aggregate)
    {
        AggregateRoot = aggregate;
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Action<TAggregate> command)
    {
        _command = command;
        return this;
    }

    public AggregateTests<TAggregate, TId> Then<TEvent>(params Action<TEvent>[] assertions)
        where TEvent : class, IDomainEvent
    {
        _command?.Invoke(AggregateRoot);

        AggregateRoot.TryDequeueEvent(out var @event).Should().BeTrue();
        @event.Should().BeOfType<TEvent>();

        foreach (var assertion in assertions)
            assertion(@event.As<TEvent>());

        return this;
    }

    public void ThenNothing()
    {
        _command?.Invoke(AggregateRoot);
        AggregateRoot.TryDequeueEvent(out _).Should().BeFalse();
    }

    public void ThenThrows<TException>(params Action<TException>[] assertions)
        where TException : Exception
    {
        var command = () => _command?.Invoke(AggregateRoot);
        var @throw = command.Should().Throw<TException>();
        var exception = @throw.Subject.First();

        foreach (var assertion in assertions)
            assertion(exception);
    }
}
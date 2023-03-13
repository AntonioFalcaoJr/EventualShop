using Contracts.Abstractions.Messages;
using Domain.Abstractions;
using Domain.Abstractions.Aggregates;
using FluentAssertions;

namespace ShoppingCartTests.Abstractions;

public abstract class AggregateTests
{
    private IAggregateRoot<IIdentifier>? _aggregateRoot;
    private ICommand? _command;

    protected AggregateTests Given<TAggregate>(params IDomainEvent[] events)
        where TAggregate : IAggregateRoot<IIdentifier>, new()
    {
        _aggregateRoot = new TAggregate();
        _aggregateRoot.LoadFromHistory(events);
        return this;
    }

    public AggregateTests When<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        _command = command;
        return this;
    }

    public AggregateTests Then<TEvent>(params Action<TEvent>[] assertions)
        where TEvent : IDomainEvent
    {
        _aggregateRoot?.Handle(_command!);

        var events = _aggregateRoot?.UncommittedEvents
            .OfType<TEvent>()
            .ToList();

        events.Should().NotBeNull();
        events.Should().NotBeEmpty();
        events.Should().ContainSingle();

        var @event = events!.First();

        if (assertions is { Length: > 0 })
            assertions.Should().AllSatisfy(assert => assert(@event));

        return this;
    }

    public void Throws<TException>(params Action<TException>[] assertions)
        where TException : Exception
    {
        var handle = () => _aggregateRoot?.Handle(_command!);
        var exceptionAssertions = handle.Should().Throw<TException>();

        if (assertions is { Length: > 0 })
            assertions.Should().AllSatisfy(assert
                => assert?.Invoke(exceptionAssertions.Subject.First()));
    }
}
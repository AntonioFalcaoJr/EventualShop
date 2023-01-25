using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using FluentAssertions;

namespace ShoppingCartTests.Abstractions;

public abstract class AggregateTests
{
    private IAggregateRoot? _aggregateRoot;
    private ICommand? _command;

    protected AggregateTests Given<TAggregate>(params IEvent[] events)
        where TAggregate : IAggregateRoot, new()
    {
        _aggregateRoot = new TAggregate();
        _aggregateRoot.Load(events);
        return this;
    }

    public AggregateTests When<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        _command = command;
        return this;
    }

    public AggregateTests Then<TEvent>(params Action<TEvent>[] assertions)
        where TEvent : IEvent
    {
        _aggregateRoot?.Handle(_command!);

        // TODO - Solve the tuple in abstraction
        var events = _aggregateRoot?.UncommittedEvents
            .Select(tuple => tuple.@event)
            .OfType<TEvent>()
            .ToList();

        events.Should().NotBeNull();
        events.Should().NotBeEmpty();
        events.Should().ContainSingle();
        
        var @event = events!.First();

        if (assertions.Any()) 
            assertions.Should().AllSatisfy(assert => assert(@event));

        return this;
    }

    public void Throws<TException>(params Action<TException>[] assertions)
        where TException : Exception
    {
        var handle = () => _aggregateRoot?.Handle(_command!);
        var exceptionAssertions = handle.Should().Throw<TException>();

        if (assertions.Any())
            assertions.Should().AllSatisfy(assert
                => assert?.Invoke(exceptionAssertions.Subject.First()));
    }
}
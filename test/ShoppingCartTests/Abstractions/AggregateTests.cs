using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using FluentAssertions;

namespace ShoppingCartTests.Abstractions;

public abstract class AggregateTests
{
    private IAggregateRoot<Guid>? _aggregateRoot;
    private ICommand? _command;

    protected AggregateTests Given<TAggregate>(params IEvent[] events)
        where TAggregate : IAggregateRoot<Guid>, new()
    {
        _aggregateRoot = new TAggregate();
        _aggregateRoot.LoadEvents(events);
        return this;
    }

    public AggregateTests When<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        _command = command;
        return this;
    }

    public void Then<TEvent>(params Action<TEvent>[] assertions)
        where TEvent : IEvent
    {
        _aggregateRoot?.Handle(_command);

        var events = _aggregateRoot?.UncommittedEvents.ToList();

        events.Should().NotBeNull();
        events.Should().AllBeOfType<TEvent>();
        events.Should().ContainSingle();

        if (assertions.Any())
            assertions.Should().AllSatisfy(assert
                => assert((TEvent)events!.First()));
    }

    public void Throws<TException>(params Action<TException>[] assertions)
        where TException : Exception
    {
        var handle = () => _aggregateRoot?.Handle(_command);
        var exceptionAssertions = handle.Should().Throw<TException>();

        if (assertions.Any())
            assertions.Should().AllSatisfy(assert
                => assert?.Invoke(exceptionAssertions.Subject.First()));
    }
}
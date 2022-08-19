using Contracts.Abstractions.Messages;

namespace Application.Abstractions.Interactors;

public interface IInteractor<in TMessage>
    where TMessage : IMessage
{
    Task InteractAsync(TMessage message, CancellationToken cancellationToken);
}

public interface ICommandInteractor<in TCommand> : IInteractor<TCommand>
    where TCommand : ICommandWithId
{
    new Task InteractAsync(TCommand command, CancellationToken cancellationToken);
}

public interface IEventInteractor<in TEvent> : IInteractor<TEvent>
    where TEvent : IEventWithId
{
    new Task InteractAsync(TEvent @event, CancellationToken cancellationToken);
}
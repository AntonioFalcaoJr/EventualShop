using Contracts.Abstractions.Messages;

namespace Application.Abstractions.Interactors;

public interface IInteractor<in TMessage>
    where TMessage : IMessage
{
    Task InteractAsync(TMessage message, CancellationToken cancellationToken);
}
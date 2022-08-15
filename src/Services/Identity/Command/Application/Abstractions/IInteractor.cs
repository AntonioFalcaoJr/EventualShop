using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions;

public interface IInteractor<in TMessage>
    where TMessage : IMessage
{
    // Task InteractAsync(TMessage message, CancellationToken cancellationToken);

    Task InteractAsync<TAggregate, TId>(TMessage message, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>, new()
        where TId : struct;
}
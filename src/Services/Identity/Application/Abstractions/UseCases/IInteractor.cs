using Contracts.Abstractions;
using Contracts.Abstractions.Messages;

namespace Application.Abstractions.UseCases;

public interface IInteractor<in TMessage>
    where TMessage : IMessage
{
    Task InteractAsync(TMessage message, CancellationToken cancellationToken);
}

public interface IInteractor<in TQuery, TProjection>
    where TQuery : IQuery
    where TProjection : IProjection
{
    Task<TProjection> InteractAsync(TQuery query, CancellationToken cancellationToken);
}
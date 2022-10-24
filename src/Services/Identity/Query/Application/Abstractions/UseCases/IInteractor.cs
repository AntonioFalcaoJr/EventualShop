using Contracts.Abstractions;
using Contracts.Abstractions.Messages;

namespace Application.Abstractions.UseCases;

public interface IInteractor<in TEvent>
    where TEvent : IEvent
{
    Task InteractAsync(TEvent @event, CancellationToken cancellationToken);
}

public interface IInteractor<in TQuery, TProjection>
    where TQuery : IQuery
    where TProjection : IProjection
{
    Task<TProjection?> InteractAsync(TQuery query, CancellationToken cancellationToken);
}
using Contracts.Abstractions;
using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Application.Abstractions;

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

public interface IPagedInteractor<in TQuery, TProjection>
    where TQuery : IQuery
    where TProjection : IProjection
{
    ValueTask<IPagedResult<TProjection>> InteractAsync(TQuery query, CancellationToken cancellationToken);
}
using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;

namespace Application.Abstractions;

public interface IInteractor<in TEvent>
    where TEvent : IEvent
{
    Task InteractAsync(TEvent @event, CancellationToken cancellationToken);
}

public interface IInteractor<in TQuery, TProjection>
    where TQuery : IQuery
{
    Task<TProjection> InteractAsync(TQuery query, CancellationToken cancellationToken);
}
using System.Threading;
using System.Threading.Tasks;
using Contracts.Abstractions.Messages;

namespace Application.Abstractions;

public interface IInteractor<in TEvent>
    where TEvent : IEvent
{
    Task InteractAsync(TEvent @event, CancellationToken cancellationToken);
}

public interface IInteractor<in TQuery, TProjection>
    where TQuery : IQuery
{
    Task<TProjection?> InteractAsync(TQuery query, CancellationToken cancellationToken);
}
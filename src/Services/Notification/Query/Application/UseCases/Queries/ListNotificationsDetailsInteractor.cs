using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Notification;

namespace Application.UseCases.Queries;

public class ListNotificationsDetailsInteractor(IProjectionGateway<Projection.NotificationDetails> projectionGateway)
    : IPagedInteractor<Query.ListNotificationsDetails, Projection.NotificationDetails>
{
    public ValueTask<IPagedResult<Projection.NotificationDetails>> InteractAsync(Query.ListNotificationsDetails query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, cancellationToken);
}
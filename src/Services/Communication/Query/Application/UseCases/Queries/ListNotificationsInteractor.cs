using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication;

namespace Application.UseCases.Queries;

public class ListNotificationsInteractor : IInteractor<Query.ListNotifications, IPagedResult<Projection.NotificationDetails>>
{
    private readonly IProjectionGateway<Projection.NotificationDetails> _projectionGateway;

    public ListNotificationsInteractor(IProjectionGateway<Projection.NotificationDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.NotificationDetails>> InteractAsync(Query.ListNotifications query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}
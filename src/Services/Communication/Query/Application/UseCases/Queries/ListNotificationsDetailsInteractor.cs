using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication;

namespace Application.UseCases.Queries;

public class ListNotificationsDetailsInteractor : IPagedInteractor<Query.ListNotificationsDetails, Projection.NotificationDetails>
{
    private readonly IProjectionGateway<Projection.NotificationDetails> _projectionGateway;

    public ListNotificationsDetailsInteractor(IProjectionGateway<Projection.NotificationDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.NotificationDetails>> InteractAsync(Query.ListNotificationsDetails query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}
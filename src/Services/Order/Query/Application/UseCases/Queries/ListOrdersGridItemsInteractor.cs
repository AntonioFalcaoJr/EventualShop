using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Order;

namespace Application.UseCases.Queries;

public class ListOrdersGridItemsInteractor : IInteractor<Query.ListOrdersGridItems, IPagedResult<Projection.OrderGridItem>>
{
    private readonly IProjectionGateway<Projection.OrderGridItem> _projectionGateway;

    public ListOrdersGridItemsInteractor(IProjectionGateway<Projection.OrderGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.OrderGridItem>?> InteractAsync(Query.ListOrdersGridItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, order => order.CustomerId == query.CustomerId, cancellationToken);
}
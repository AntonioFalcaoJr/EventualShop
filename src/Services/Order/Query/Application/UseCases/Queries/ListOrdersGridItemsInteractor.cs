using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Order;

namespace Application.UseCases.Queries;

public class ListOrdersGridItemsInteractor : IPagedInteractor<Query.ListOrdersGridItems, Projection.OrderGridItem>
{
    private readonly IProjectionGateway<Projection.OrderGridItem> _projectionGateway;

    public ListOrdersGridItemsInteractor(IProjectionGateway<Projection.OrderGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.OrderGridItem>> InteractAsync(Query.ListOrdersGridItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, order => order.CustomerId == query.CustomerId, cancellationToken);
}
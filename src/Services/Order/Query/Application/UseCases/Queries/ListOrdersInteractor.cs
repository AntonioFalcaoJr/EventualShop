using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Order;

namespace Application.UseCases.Queries;

public class ListOrdersInteractor : IInteractor<Query.ListOrders, IPagedResult<Projection.OrderDetails>>
{
    private readonly IProjectionGateway<Projection.OrderDetails> _projectionGateway;

    public ListOrdersInteractor(IProjectionGateway<Projection.OrderDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.OrderDetails>> InteractAsync(Query.ListOrders query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}
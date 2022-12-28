using Application.Abstractions;
using Contracts.Services.Order;

namespace Application.UseCases.Queries;

public class GetOrderDetailsInteractor : IInteractor<Query.GetOrderDetails, Projection.OrderDetails>
{
    private readonly IProjectionGateway<Projection.OrderDetails> _projectionGateway;

    public GetOrderDetailsInteractor(IProjectionGateway<Projection.OrderDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.OrderDetails> InteractAsync(Query.GetOrderDetails query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.OrderId, cancellationToken);
}
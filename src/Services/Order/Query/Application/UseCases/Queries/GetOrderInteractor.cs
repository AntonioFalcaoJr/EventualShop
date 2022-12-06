using Application.Abstractions;
using Contracts.Services.Order;

namespace Application.UseCases.Queries;

public class GetOrderInteractor : IInteractor<Query.GetOrder, Projection.OrderDetails>
{
    private readonly IProjectionGateway<Projection.OrderDetails> _projectionGateway;

    public GetOrderInteractor(IProjectionGateway<Projection.OrderDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.OrderDetails> InteractAsync(Query.GetOrder query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.OrderId, cancellationToken);
}
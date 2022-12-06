using Application.Abstractions;
using Contracts.Services.Order;

namespace Application.UseCases.Events;

public class OrderConfirmedInteractor : IInteractor<DomainEvent.OrderConfirmed>
{
    private readonly IProjectionGateway<Projection.OrderDetails> _projectionGateway;

    public OrderConfirmedInteractor(IProjectionGateway<Projection.OrderDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.OrderConfirmed @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(@event.OrderId, order => order.Status, @event.Status, cancellationToken);
}
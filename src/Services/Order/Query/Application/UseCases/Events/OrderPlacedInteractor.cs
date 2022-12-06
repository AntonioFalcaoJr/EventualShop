using Application.Abstractions;
using Contracts.Services.Order;

namespace Application.UseCases.Events;

public class OrderPlacedInteractor : IInteractor<DomainEvent.OrderPlaced>
{
    private readonly IProjectionGateway<Projection.OrderDetails> _projectionGateway;

    public OrderPlacedInteractor(IProjectionGateway<Projection.OrderDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.OrderPlaced @event, CancellationToken cancellationToken)
    {
        Projection.OrderDetails orderDetails =
            new(@event.OrderId,
                @event.CustomerId,
                @event.Total,
                @event.BillingAddress,
                @event.ShippingAddress,
                @event.Items,
                @event.PaymentMethods,
                @event.Status,
                false);

        await _projectionGateway.InsertAsync(orderDetails, cancellationToken);
    }
}
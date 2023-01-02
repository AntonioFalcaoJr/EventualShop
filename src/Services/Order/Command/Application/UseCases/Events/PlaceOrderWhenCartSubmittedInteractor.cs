using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Contracts.Services.Order;
using ShoppingCart = Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IPlaceOrderWhenCartSubmittedInteractor : IInteractor<ShoppingCart.IntegrationEvent.CartSubmitted> { }

public class PlaceOrderWhenCartSubmittedInteractor : IPlaceOrderWhenCartSubmittedInteractor
{
    private readonly IApplicationService _applicationService;

    public PlaceOrderWhenCartSubmittedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(ShoppingCart.IntegrationEvent.CartSubmitted @event, CancellationToken cancellationToken)
    {
        Order order = new();

        var command = new Command.PlaceOrder(
            @event.CartId,
            @event.CustomerId,
            @event.Total,
            @event.BillingAddress,
            @event.ShippingAddress,
            @event.Items,
            @event.PaymentMethods);

        order.Handle(command);

        await _applicationService.AppendEventsAsync(order, cancellationToken);
    }
}
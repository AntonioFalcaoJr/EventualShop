using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Contracts.Services.Order;
using ShoppingCart = Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IPlaceOrderWhenCartSubmittedInteractor : IInteractor<ShoppingCart.SummaryEvent.CartSubmitted> { }

public class PlaceOrderWhenCartSubmittedInteractor : IPlaceOrderWhenCartSubmittedInteractor
{
    private readonly IApplicationService _applicationService;

    public PlaceOrderWhenCartSubmittedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(ShoppingCart.SummaryEvent.CartSubmitted @event, CancellationToken cancellationToken)
    {
        Order order = new();

        var command = new Command.PlaceOrder(
            @event.Cart.Id,
            @event.Cart.CustomerId,
            @event.Cart.Total,
            @event.Cart.BillingAddress!,
            @event.Cart.ShippingAddress!,
            @event.Cart.Items,
            @event.Cart.PaymentMethods);

        order.Handle(command);

        await _applicationService.AppendEventsAsync(order, cancellationToken);
    }
}
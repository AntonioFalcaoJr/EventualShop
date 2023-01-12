using Application.Abstractions;
using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public interface IPublishCartSubmittedWhenCartCheckedOutInteractor : IInteractor<DomainEvent.CartCheckedOut> { }

public class PublishCartSubmittedWhenCartCheckedOutInteractor : IPublishCartSubmittedWhenCartCheckedOutInteractor
{
    private readonly IApplicationService _applicationService;

    public PublishCartSubmittedWhenCartCheckedOutInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.CartCheckedOut @event, CancellationToken cancellationToken)
    {
        var shoppingCart = await _applicationService.LoadAggregateAsync<ShoppingCart>(@event.CartId, cancellationToken);

        IntegrationEvent.CartSubmitted cartSubmitted = new(
            shoppingCart.Id,
            shoppingCart.CustomerId,
            shoppingCart.Total,
            shoppingCart.BillingAddress!,
            shoppingCart.ShippingAddress!,
            shoppingCart.Items.Select(item => (Dto.CartItem)item),
            shoppingCart.PaymentMethods.Select(method => (Dto.PaymentMethod)method));

        await _applicationService.PublishEventAsync(cartSubmitted, cancellationToken);
    }
}
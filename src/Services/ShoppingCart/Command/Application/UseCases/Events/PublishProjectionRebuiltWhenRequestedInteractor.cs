using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public interface IPublishCartProjectionRebuiltWhenRequestedInteractor : IInteractor<NotificationEvent.CartProjectionRebuildRequested> { }

public class PublishCartProjectionRebuiltWhenRequestedInteractor : IPublishCartProjectionRebuiltWhenRequestedInteractor
{
    private readonly IApplicationService _applicationService;

    public PublishCartProjectionRebuiltWhenRequestedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(NotificationEvent.CartProjectionRebuildRequested @event, CancellationToken cancellationToken)
    {
        var shoppingCart = await _applicationService.LoadAggregateAsync<ShoppingCart>(@event.CartId, cancellationToken);
        SummaryEvent.CartProjectionRebuilt cartProjectionRebuilt = new(shoppingCart, shoppingCart.Version);
        await _applicationService.PublishEventAsync(cartProjectionRebuilt, cancellationToken);
    }
}
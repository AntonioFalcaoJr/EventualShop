using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Contracts.DataTransferObjects;
using Domain.Aggregates.ShoppingCarts;

namespace Application.UseCases.ShoppingCarts.Events;

public interface IPublishCartProjectionRebuiltWhenRequestedInteractor : IInteractor<NotificationEvent.CartProjectionRebuildRequested>;

public class PublishCartProjectionRebuiltWhenRequestedInteractor(IApplicationService service) : IPublishCartProjectionRebuiltWhenRequestedInteractor
{
    public async Task InteractAsync(NotificationEvent.CartProjectionRebuildRequested cmd, CancellationToken cancellationToken)
    {
        var shoppingCart = await service.LoadAggregateAsync<ShoppingCart, CartId>((CartId)cmd.CartId, cancellationToken);

        // TODO: Solve the projection rebuilding strategy 
        Dto.ShoppingCart cart = default!;

        SummaryEvent.CartProjectionRebuilt cartProjectionRebuilt = new(cart, shoppingCart.Version);
        await service.PublishEventAsync(cartProjectionRebuilt, cancellationToken);
    }
}
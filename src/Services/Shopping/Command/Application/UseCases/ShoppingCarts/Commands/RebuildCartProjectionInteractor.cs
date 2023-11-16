using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Domain.Aggregates.ShoppingCarts;

namespace Application.UseCases.ShoppingCarts.Commands;

public class RebuildCartProjectionInteractor(IApplicationService service) : IInteractor<Command.RebuildCartProjection>
{
    public async Task InteractAsync(Command.RebuildCartProjection cmd, CancellationToken cancellationToken)
    {
        await foreach (var cartId in service.StreamAggregatesId<ShoppingCart, CartId>().WithCancellation(cancellationToken))
            await service.PublishEventAsync(new NotificationEvent.CartProjectionRebuildRequested(cartId, cmd.Projection), cancellationToken);
    }
}
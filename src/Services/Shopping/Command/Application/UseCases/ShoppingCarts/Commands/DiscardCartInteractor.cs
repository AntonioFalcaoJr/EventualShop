using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Domain.Aggregates.ShoppingCarts;

namespace Application.UseCases.ShoppingCarts.Commands;

public class DiscardCartInteractor(IApplicationService service) : IInteractor<Command.DiscardCart>
{
    public async Task InteractAsync(Command.DiscardCart cmd, CancellationToken cancellationToken)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>((CartId)cmd.CartId, cancellationToken);
        cart.Discard();
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellationToken);
    }
}
using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Domain.Aggregates.ShoppingCarts;

namespace Application.UseCases.ShoppingCarts.Commands;

public class CheckOutCartInteractor(IApplicationService service) : IInteractor<Command.CheckOutCart>
{
    public async Task InteractAsync(Command.CheckOutCart cmd, CancellationToken cancellationToken)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>((CartId)cmd.CartId, cancellationToken);
        cart.CheckOut();
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellationToken);
    }
}
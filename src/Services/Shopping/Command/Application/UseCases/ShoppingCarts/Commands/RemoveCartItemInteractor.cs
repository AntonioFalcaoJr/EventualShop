using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;

namespace Application.UseCases.ShoppingCarts.Commands;

public class RemoveCartItemInteractor(IApplicationService service) : IInteractor<Command.RemoveCartItem>
{
    public async Task InteractAsync(Command.RemoveCartItem cmd, CancellationToken cancellationToken)
    {
        var shoppingCart = await service.LoadAggregateAsync<ShoppingCart, CartId>((CartId)cmd.CartId, cancellationToken);
        shoppingCart.RemoveItem((ProductId)cmd.ProductId);
        await service.AppendEventsAsync<ShoppingCart, CartId>(shoppingCart, cancellationToken);
    }
}
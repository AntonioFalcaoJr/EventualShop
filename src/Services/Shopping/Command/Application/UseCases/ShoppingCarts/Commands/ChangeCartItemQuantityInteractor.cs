using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;
using Domain.ValueObjects;

namespace Application.UseCases.ShoppingCarts.Commands;

public class ChangeCartItemQuantityInteractor(IApplicationService service) : IInteractor<Command.ChangeCartItemQuantity>
{
    public async Task InteractAsync(Command.ChangeCartItemQuantity cmd, CancellationToken cancellationToken)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>((CartId)cmd.CartId, cancellationToken);
        
        cart.ChangeItemQuantity(
            (ProductId)cmd.ProductId, 
            (Quantity)cmd.NewQuantity);
        
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellationToken);
    }
}
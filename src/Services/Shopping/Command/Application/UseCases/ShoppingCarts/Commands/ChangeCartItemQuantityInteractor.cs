using Application.Services;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Commands;

public record ChangeCartItemQuantity(CartId CartId, ProductId ProductId, Quantity NewQuantity) : IRequest;

public class ChangeCartItemQuantityInteractor(IApplicationService service) : IRequestHandler<ChangeCartItemQuantity>
{
    public async Task Handle(ChangeCartItemQuantity cmd, CancellationToken token)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>(cmd.CartId, token);
        var product = await service.LoadAggregateAsync<Product, ProductId>(cmd.ProductId, token);

        product.ReserveStock(cmd.CartId, cmd.NewQuantity);
        cart.ChangeItemQuantity(cmd.ProductId, cmd.NewQuantity);

        await service.AppendEventsAsync<Product, ProductId>(product, token);
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, token);
    }
}
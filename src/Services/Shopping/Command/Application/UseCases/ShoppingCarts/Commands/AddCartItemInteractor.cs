using Application.Services;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;
using Domain.Entities.CartItems;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Commands;

public record AddCartItem(CartId CartId, ProductId ProductId, Quantity Quantity) : IRequest<CartItemId>;

public class AddCartItemInteractor(IApplicationService service) : IRequestHandler<AddCartItem, CartItemId>
{
    public async Task<CartItemId> Handle(AddCartItem cmd, CancellationToken cancellationToken)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>(cmd.CartId, cancellationToken);
        var product = await service.LoadAggregateAsync<Product, ProductId>(cmd.ProductId, cancellationToken);

        ArgumentOutOfRangeException.ThrowIfGreaterThan<int>(cmd.Quantity, product.Stock, "Product stock is not enough.");

        CartItem newItem = new(
            CartItemId.New,
            product.Id,
            product.Name,
            product.PictureUri,
            product.Sku,
            product.Prices,
            cmd.Quantity);

        cart.AddItem(newItem);

        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellationToken);

        return newItem.Id;
    }
}
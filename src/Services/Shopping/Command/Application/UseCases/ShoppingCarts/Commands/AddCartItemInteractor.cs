using Application.Services;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;
using Domain.Entities.CartItems;
using Domain.Enumerations;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Commands;

public record AddCartItem(CartId CartId, CustomerId CustomerId, ProductId ProductId, Quantity Quantity) : IRequest<CartItemId>;

public class AddCartItemInteractor(IApplicationService service) : IRequestHandler<AddCartItem, CartItemId>
{
    // The Handle method is decorated with a retry policy for OptimisticConcurrencyException.
    // May throw TooManyConcurrencyConflicts if retries exceed limits.
    public async Task<CartItemId> Handle(AddCartItem cmd, CancellationToken token)
    {
        var cart = await service.LoadOrInitializeAggregateAsync<ShoppingCart, CartId>(cmd.CartId, token);
        if (cart.Status is CartFree) cart.StartShopping(cmd.CustomerId);

        // Throws ProductNotFound if the ProductId does not exist.
        var product = await service.LoadAggregateAsync<Product, ProductId>(cmd.ProductId, token);

        CartItem newItem = new(
            CartItemId.New,
            product.Id,
            product.Name,
            product.PictureUri,
            product.Sku,
            product.Prices,
            cmd.Quantity);

        // Throws InsufficientStock if the requested quantity exceeds available stock.
        product.ReserveStock(cart.Id, cmd.Quantity); // 10-minutes TTL
        // Throws CartIsFull if the cart has reached its item limit.
        // Throws CartIsClosed if the cart is not open for modifications.
        cart.AddItem(newItem);

        // Persist first since it has higher concurrency risks.
        // Throws OptimisticConcurrency if the version check fails.
        await service.AppendEventsAsync<Product, ProductId>(product, token);
        // Persist second since it has lower concurrency risks.
        // Throws OptimisticConcurrency if the version check fails.
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, token);

        return newItem.Id;
    }
}
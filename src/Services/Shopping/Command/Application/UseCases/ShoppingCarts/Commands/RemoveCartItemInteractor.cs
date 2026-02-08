using Application.Services;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Commands;

public record RemoveCartItem(CartId CartId, ProductId ProductId) : IRequest;

public class RemoveCartItemInteractor(IApplicationService service) : IRequestHandler<RemoveCartItem>
{
    public async Task Handle(RemoveCartItem cmd, CancellationToken cancellationToken)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>(cmd.CartId, cancellationToken);
        var product = await service.LoadAggregateAsync<Product, ProductId>(cmd.ProductId, cancellationToken);

        var quantity = cart.Items[cmd.ProductId].Quantity;

        cart.RemoveItem(cmd.ProductId);
        product.Restock(quantity);

        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellationToken);
        await service.AppendEventsAsync<Product, ProductId>(product, cancellationToken);
    }
}
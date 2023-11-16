using Application.Services;
using Domain.Aggregates.ShoppingCarts;
using Domain.Enumerations;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Commands;

public record StartShopping(CustomerId CustomerId) : IRequest<CartId>;

public class StartShoppingInteractor(IApplicationService service) : IRequestHandler<StartShopping, CartId>
{
    public async Task<CartId> Handle(StartShopping cmd, CancellationToken cancellationToken)
    {
        var cart = await service.LoadAggregateAsync<ShoppingCart, CartId>(cart => cart.CustomerId == cmd.CustomerId, cancellationToken);
        if (cart.Status is CartStatusOpen or CartStatusAbandoned) return cart.Id;

        cart = ShoppingCart.StartShopping(cmd.CustomerId);
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellationToken);
        return cart.Id;
    }
}
using Application.Services;
using Domain.Aggregates.ShoppingCarts;
using MediatR;

namespace Application.UseCases.ShoppingCarts.Commands;

public record StartShopping(CustomerId CustomerId) : IRequest<CartId>;

public class StartShoppingInteractor(IApplicationService service) : IRequestHandler<StartShopping, CartId>
{
    public async Task<CartId> Handle(StartShopping cmd, CancellationToken cancellation)
    {
        ShoppingCart cart = new();
        cart.StartShopping(cmd.CustomerId);
        await service.AppendEventsAsync<ShoppingCart, CartId>(cart, cancellation);
        return cart.Id;
    }
}
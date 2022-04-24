using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetShoppingCartItemConsumer :
    IConsumer<Query.GetShoppingCartItem>,
    IConsumer<Query.GetShoppingCartItems>
{
    private readonly IProjectionRepository<Projection.ShoppingCartItem> _projectionRepository;

    public GetShoppingCartItemConsumer(IProjectionRepository<Projection.ShoppingCartItem> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartItem> context)
    {
        var shoppingCartItem = await _projectionRepository.FindAsync(
            item => item.ShoppingCartId == context.Message.CartId && item.Id == context.Message.ItemId, context.CancellationToken);

        await (shoppingCartItem is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItem));
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartItems> context)
    {
        var shoppingCartItems = await _projectionRepository.GetAllAsync(
            context.Message.Limit,
            context.Message.Offset,
            item => item.ShoppingCartId == context.Message.CartId,
            context.CancellationToken);

        await (shoppingCartItems is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItems));
    }
}
using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetShoppingCartItemConsumer :
    IConsumer<Query.GetShoppingCartItem>,
    IConsumer<Query.GetShoppingCartItems>
{
    private readonly IProjectionRepository<Projection.ShoppingCartItem> _repository;

    public GetShoppingCartItemConsumer(IProjectionRepository<Projection.ShoppingCartItem> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartItem> context)
    {
        var shoppingCartItem = await _repository.FindAsync(
            item => item.CartId == context.Message.CartId && item.Id == context.Message.ItemId, context.CancellationToken);

        await (shoppingCartItem is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItem));
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartItems> context)
    {
        var shoppingCartItems = await _repository.GetAllAsync(
            context.Message.Limit,
            context.Message.Offset,
            item => item.CartId == context.Message.CartId,
            context.CancellationToken);

        await (shoppingCartItems is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItems));
    }
}
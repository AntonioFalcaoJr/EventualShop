using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartItemConsumer :
    IConsumer<Queries.GetShoppingCartItem>,
    IConsumer<Queries.GetShoppingCartItems>
{
    private readonly IProjectionsRepository<Projections.ShoppingCartItem> _projectionsRepository;

    public GetShoppingCartItemConsumer(IProjectionsRepository<Projections.ShoppingCartItem> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCartItem> context)
    {
        var shoppingCartItem = await _projectionsRepository.FindAsync(
            item => item.ShoppingCartId == context.Message.CartId && item.Id == context.Message.ItemId, context.CancellationToken);

        await (shoppingCartItem is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItem));
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCartItems> context)
    {
        var shoppingCartItems = await _projectionsRepository.GetAllAsync(
            context.Message.Limit,
            context.Message.Offset,
            item => item.ShoppingCartId == context.Message.CartId,
            context.CancellationToken);

        await (shoppingCartItems is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItems));
    }
}
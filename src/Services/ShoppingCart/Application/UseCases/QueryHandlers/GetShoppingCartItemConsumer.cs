using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartItemConsumer : IConsumer<Queries.GetShoppingCartItem>
{
    private readonly IShoppingCartProjectionsService _projectionsService;

    public GetShoppingCartItemConsumer(IShoppingCartProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCartItem> context)
    {
        var shoppingCartItemProjection = await _projectionsService.GetShoppingCartItemAsync(context.Message.CartId, context.Message.ItemId, context.CancellationToken);

        await (shoppingCartItemProjection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync<Responses.ShoppingCartItem>(shoppingCartItemProjection));
    }
}
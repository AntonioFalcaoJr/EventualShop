using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;
using GetShoppingCartItemQuery = ECommerce.Contracts.ShoppingCart.Queries.GetShoppingCartItem;

namespace Application.UseCases.Queries;

public class GetShoppingCartItemConsumer : IConsumer<GetShoppingCartItemQuery>
{
    private readonly IShoppingCartProjectionsService _projectionsService;

    public GetShoppingCartItemConsumer(IShoppingCartProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetShoppingCartItemQuery> context)
    {
        var shoppingCartItemProjection = await _projectionsService.GetShoppingCartItemAsync(context.Message.CartId, context.Message.ItemId, context.CancellationToken);

        await (shoppingCartItemProjection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync<Responses.ShoppingCartItem>(shoppingCartItemProjection));
    }
}
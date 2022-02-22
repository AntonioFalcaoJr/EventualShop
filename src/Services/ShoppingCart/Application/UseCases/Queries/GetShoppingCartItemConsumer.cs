using System.Threading.Tasks;
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
        var cartItemProjection = await _projectionsService.GetCartItemAsync(context.Message.CartId, context.Message.ItemId, context.CancellationToken);

        await (cartItemProjection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync<Responses.CartItem>(cartItemProjection));
    }
}
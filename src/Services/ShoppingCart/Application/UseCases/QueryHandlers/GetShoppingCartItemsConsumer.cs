using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartItemsConsumer : IConsumer<Queries.GetShoppingCartItems>
{
    private readonly IShoppingCartProjectionsService _projectionsService;

    public GetShoppingCartItemsConsumer(IShoppingCartProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCartItems> context)
    {
        var shoppingCartItemsProjection = await _projectionsService
            .GetShoppingCartItemsAsync(context.Message.CartId, context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (shoppingCartItemsProjection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync<Responses.ShoppingCartItems>(shoppingCartItemsProjection));
    }
}
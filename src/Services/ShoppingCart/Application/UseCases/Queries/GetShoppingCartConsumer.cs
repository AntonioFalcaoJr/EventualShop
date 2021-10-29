using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.ShoppingCarts;
using GetShoppingCartQuery = Messages.ShoppingCarts.Queries.GetShoppingCart;

namespace Application.UseCases.Queries;

public class GetShoppingCartConsumer : IConsumer<GetShoppingCartQuery>
{
    private readonly IShoppingCartProjectionsService _projectionsService;

    public GetShoppingCartConsumer(IShoppingCartProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetShoppingCartQuery> context)
    {
        var cartDetails = await _projectionsService.GetCartDetailsAsync(context.Message.UserId, context.CancellationToken);
        await context.RespondAsync<Responses.CartDetails>(cartDetails);
    }
}
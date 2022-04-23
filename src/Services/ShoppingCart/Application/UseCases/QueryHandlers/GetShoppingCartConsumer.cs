using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartConsumer :
    IConsumer<Queries.GetShoppingCart>,
    IConsumer<Queries.GetCustomerShoppingCart>
{
    private readonly IProjectionRepository<Projections.ShoppingCart> _projectionRepository;

    public GetShoppingCartConsumer(IProjectionRepository<Projections.ShoppingCart> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCustomerShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionRepository.FindAsync(cart => cart.Customer.Id == context.Message.CustomerId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionRepository.GetAsync(context.Message.CartId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    private static Task RespondAsync(Projections.ShoppingCart projection, ConsumeContext context)
        => projection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(projection);
}
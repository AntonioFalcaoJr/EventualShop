using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartDetailsConsumer :
    IConsumer<Queries.GetShoppingCart>,
    IConsumer<Queries.GetCustomerShoppingCart>
{
    private readonly IProjectionsRepository<Projections.ShoppingCart> _projectionsRepository;

    public GetShoppingCartDetailsConsumer(IProjectionsRepository<Projections.ShoppingCart> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCustomerShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionsRepository.FindAsync(cart => cart.Customer.Id == context.Message.CustomerId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionsRepository.GetAsync(context.Message.CartId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    private static Task RespondAsync(Projections.ShoppingCart projection, ConsumeContext context)
        => projection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(projection);
}
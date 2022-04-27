using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetShoppingCartConsumer :
    IConsumer<Query.GetShoppingCart>,
    IConsumer<Query.GetCustomerShoppingCart>
{
    private readonly IProjectionRepository<Projection.ShoppingCart> _repository;

    public GetShoppingCartConsumer(IProjectionRepository<Projection.ShoppingCart> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetCustomerShoppingCart> context)
    {
        var shoppingCartProjection = await _repository.FindAsync(cart => cart.Customer.Id == context.Message.CustomerId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCart> context)
    {
        var shoppingCartProjection = await _repository.GetAsync(context.Message.CartId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    private static Task RespondAsync(Projection.ShoppingCart projection, ConsumeContext context)
        => projection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(projection);
}
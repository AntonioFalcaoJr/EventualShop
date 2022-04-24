﻿using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetShoppingCartConsumer :
    IConsumer<Query.GetShoppingCart>,
    IConsumer<Query.GetCustomerShoppingCart>
{
    private readonly IProjectionRepository<Projection.ShoppingCart> _projectionRepository;

    public GetShoppingCartConsumer(IProjectionRepository<Projection.ShoppingCart> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Query.GetCustomerShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionRepository.FindAsync(cart => cart.Customer.Id == context.Message.CustomerId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionRepository.GetAsync(context.Message.CartId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    private static Task RespondAsync(Projection.ShoppingCart projection, ConsumeContext context)
        => projection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(projection);
}
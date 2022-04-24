﻿using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Commands;

public class DecreaseShoppingCartItemConsumer : IConsumer<Command.DecreaseShoppingCartItem>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public DecreaseShoppingCartItemConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DecreaseShoppingCartItem> context)
    {
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendEventsAsync(shoppingCart, context.CancellationToken);
    }
}
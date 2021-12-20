﻿using Application.EventSourcing.EventStore;
using MassTransit;
using RemoveCartItemCommand = ECommerce.Contracts.ShoppingCart.Commands.RemoveCartItem;

namespace Application.UseCases.Commands;

public class RemoveCartItemConsumer : IConsumer<RemoveCartItemCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public RemoveCartItemConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<RemoveCartItemCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
    }
}
﻿using Application.UseCases.Events;
using MassTransit;
using ShoppingCart = Contracts.Services.ShoppingCart;

namespace Infrastructure.MessageBus.Consumers.Events;

public class PlaceOrderWhenCartSubmittedConsumer : IConsumer<ShoppingCart.IntegrationEvent.CartSubmitted>
{
    private readonly IPlaceOrderWhenCartSubmittedInteractor _interactor;

    public PlaceOrderWhenCartSubmittedConsumer(IPlaceOrderWhenCartSubmittedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<ShoppingCart.IntegrationEvent.CartSubmitted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}
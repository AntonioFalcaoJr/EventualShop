using Application.UseCases.ShoppingCarts.Events;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class PublishCartProjectionRebuiltWhenRequestedConsumer(IPublishCartProjectionRebuiltWhenRequestedInteractor interactor)
    : Consumer<NotificationEvent.CartProjectionRebuildRequested>(interactor);
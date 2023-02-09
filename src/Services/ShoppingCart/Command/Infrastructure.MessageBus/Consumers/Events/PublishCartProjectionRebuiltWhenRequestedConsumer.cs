using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class PublishCartProjectionRebuiltWhenRequestedConsumer : Consumer<NotificationEvent.CartProjectionRebuildRequested>
{
    public PublishCartProjectionRebuiltWhenRequestedConsumer(IPublishCartProjectionRebuiltWhenRequestedInteractor interactor)
        : base(interactor) { }
}
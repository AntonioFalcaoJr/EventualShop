using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class PublishCartSubmittedWhenCartCheckedOutConsumer : Consumer<DomainEvent.CartCheckedOut>
{
    public PublishCartSubmittedWhenCartCheckedOutConsumer(IPublishCartSubmittedWhenCartCheckedOutInteractor interactor)
        : base(interactor) { }
}
using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class CartSubmittedConsumer : Consumer<IntegrationEvent.CartSubmitted>
{
    public CartSubmittedConsumer(IInteractor<IntegrationEvent.CartSubmitted> interactor)
        : base(interactor) { }
}
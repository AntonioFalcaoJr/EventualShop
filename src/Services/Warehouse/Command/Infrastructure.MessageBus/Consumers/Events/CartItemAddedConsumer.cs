using Application.UseCases.Events;
using Infrastructure.MessageBus.Abstractions;
using ShoppingCart = Contracts.Services.ShoppingCart;

namespace Infrastructure.MessageBus.Consumers.Events;

public class CartItemAddedConsumer : Consumer<ShoppingCart.DomainEvent.CartItemAdded>
{
    public CartItemAddedConsumer(IReserveInventoryItemWhenCartItemAddedInteractor interactor)
        : base(interactor) { }
}
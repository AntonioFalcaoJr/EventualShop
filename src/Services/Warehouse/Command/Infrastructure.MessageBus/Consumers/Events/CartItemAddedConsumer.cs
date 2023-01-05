using Application.Abstractions;
using Infrastructure.MessageBus.Abstractions;
using ShoppingCart = Contracts.Services.ShoppingCart;

namespace Infrastructure.MessageBus.Consumers.Events;

public class CartItemAddedConsumer : Consumer<ShoppingCart.DomainEvent.CartItemAdded>
{
    public CartItemAddedConsumer(IInteractor<ShoppingCart.DomainEvent.CartItemAdded> interactor)
        : base(interactor) { }
}
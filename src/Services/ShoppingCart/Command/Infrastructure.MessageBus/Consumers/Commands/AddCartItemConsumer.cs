using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddCartItemConsumer : Consumer<Command.AddCartItem>
{
    public AddCartItemConsumer(IInteractor<Command.AddCartItem> interactor)
        : base(interactor) { }
}
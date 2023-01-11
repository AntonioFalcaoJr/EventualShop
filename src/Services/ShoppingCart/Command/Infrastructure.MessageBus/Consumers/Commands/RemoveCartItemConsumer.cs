using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RemoveCartItemConsumer : Consumer<Command.RemoveCartItem>
{
    public RemoveCartItemConsumer(IInteractor<Command.RemoveCartItem> interactor)
        : base(interactor) { }
}
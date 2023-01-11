using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangeCartItemQuantityConsumer : Consumer<Command.ChangeCartItemQuantity>
{
    public ChangeCartItemQuantityConsumer(IInteractor<Command.ChangeCartItemQuantity> interactor)
        : base(interactor) { }
}
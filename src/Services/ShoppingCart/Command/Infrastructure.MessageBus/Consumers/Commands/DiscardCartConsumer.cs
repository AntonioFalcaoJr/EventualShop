using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DiscardCartConsumer : Consumer<Command.DiscardCart>
{
    public DiscardCartConsumer(IInteractor<Command.DiscardCart> interactor)
        : base(interactor) { }
}
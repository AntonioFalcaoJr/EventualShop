using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class CreateCartConsumer : Consumer<Command.CreateCart>
{
    public CreateCartConsumer(IInteractor<Command.CreateCart> interactor)
        : base(interactor) { }
}
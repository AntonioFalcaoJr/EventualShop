using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddPayPalConsumer : Consumer<Command.AddPayPal>
{
    public AddPayPalConsumer(IInteractor<Command.AddPayPal> interactor)
        : base(interactor) { }
}
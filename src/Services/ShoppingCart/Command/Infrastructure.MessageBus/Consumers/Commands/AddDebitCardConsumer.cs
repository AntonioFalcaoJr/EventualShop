using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddDebitCardConsumer : Consumer<Command.AddDebitCard>
{
    public AddDebitCardConsumer(IInteractor<Command.AddDebitCard> interactor)
        : base(interactor) { }
}
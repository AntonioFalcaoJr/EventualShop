using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddCreditCardConsumer : Consumer<Command.AddCreditCard>
{
    public AddCreditCardConsumer(IInteractor<Command.AddCreditCard> interactor)
        : base(interactor) { }
}
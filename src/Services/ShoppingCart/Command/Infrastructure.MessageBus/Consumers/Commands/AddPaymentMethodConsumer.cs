using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddPaymentMethodConsumer : Consumer<Command.AddPaymentMethod>
{
    public AddPaymentMethodConsumer(IInteractor<Command.AddPaymentMethod> interactor)
        : base(interactor) { }
}
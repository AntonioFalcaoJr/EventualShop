using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RemovePaymentMethodConsumer : Consumer<Command.RemovePaymentMethod>
{
    public RemovePaymentMethodConsumer(IInteractor<Command.RemovePaymentMethod> interactor)
        : base(interactor) { }
}
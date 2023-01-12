using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddShippingAddressConsumer : Consumer<Command.AddShippingAddress>
{
    public AddShippingAddressConsumer(IInteractor<Command.AddShippingAddress> interactor)
        : base(interactor) { }
}
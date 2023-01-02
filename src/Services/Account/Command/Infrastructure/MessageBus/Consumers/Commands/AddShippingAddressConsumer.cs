using Application.Abstractions;
using Contracts.Services.Account;
using MessageBus.Abstractions;

namespace MessageBus.Consumers.Commands;

public class AddShippingAddressConsumer : Consumer<Command.AddShippingAddress>
{
    public AddShippingAddressConsumer(IInteractor<Command.AddShippingAddress> interactor)
        : base(interactor) { }
}
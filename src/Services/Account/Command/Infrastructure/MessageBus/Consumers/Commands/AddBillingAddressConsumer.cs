using Application.Abstractions;
using Contracts.Services.Account;
using MessageBus.Abstractions;

namespace MessageBus.Consumers.Commands;

public class AddBillingAddressConsumer : Consumer<Command.AddBillingAddress>
{
    public AddBillingAddressConsumer(IInteractor<Command.AddBillingAddress> interactor)
        : base(interactor) { }
}
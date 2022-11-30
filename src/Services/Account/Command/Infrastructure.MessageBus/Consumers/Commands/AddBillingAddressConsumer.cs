using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddBillingAddressConsumer : Consumer<Command.AddBillingAddress>
{
    public AddBillingAddressConsumer(IInteractor<Command.AddBillingAddress> interactor)
        : base(interactor) { }
}
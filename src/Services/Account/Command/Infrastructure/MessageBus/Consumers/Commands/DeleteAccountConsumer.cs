using Application.Abstractions;
using Contracts.Services.Account;
using MessageBus.Abstractions;

namespace MessageBus.Consumers.Commands;

public class DeleteAccountConsumer : Consumer<Command.DeleteAccount>
{
    public DeleteAccountConsumer(IInteractor<Command.DeleteAccount> interactor)
        : base(interactor) { }
}
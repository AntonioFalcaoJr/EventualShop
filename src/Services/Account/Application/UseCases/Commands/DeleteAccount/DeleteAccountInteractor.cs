using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Account;

namespace Application.UseCases.Commands.DeleteAccount;

public class DeleteAccountInteractor : IInteractor<Command.DeleteAccount>
{
    private readonly IAccountEventStoreService _eventStore;

    public DeleteAccountInteractor(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task InteractAsync(Command.DeleteAccount command, CancellationToken ct)
    {
        var account = await _eventStore.LoadAsync(command.AccountId, ct);
        account.Handle(command);
        await _eventStore.AppendAsync(account, ct);
    }
}
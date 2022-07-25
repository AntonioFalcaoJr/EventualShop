using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Account;

namespace Application.UseCases.Commands.AddBillingAddress;

public class AddBillingAddressInteractor : IInteractor<Command.AddBillingAddress>
{
    private readonly IAccountEventStoreService _eventStore;

    public AddBillingAddressInteractor(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task InteractAsync(Command.AddBillingAddress command, CancellationToken ct)
    {
        var account = await _eventStore.LoadAsync(command.AccountId, ct);
        account.Handle(command);
        await _eventStore.AppendAsync(account, ct);
    }
}
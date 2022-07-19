using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Account;

namespace Application.UseCases.Commands;

public class AddBillingAddressInteractor : IInteractor<Command.AddBillingAddress>
{
    private readonly IAccountEventStoreService _eventStore;

    public AddBillingAddressInteractor(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task InteractAsync(Command.AddBillingAddress command, CancellationToken cancellationToken)
    {
        var account = await _eventStore.LoadAggregateAsync(command.AccountId, cancellationToken);
        account.Handle(command);
        await _eventStore.AppendEventsAsync(account, cancellationToken);
    }
}
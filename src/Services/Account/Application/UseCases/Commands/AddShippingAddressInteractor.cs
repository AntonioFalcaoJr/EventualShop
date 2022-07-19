using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Account;

namespace Application.UseCases.Commands;

public class AddShippingAddressInteractor : IInteractor<Command.AddShippingAddress>
{
    private readonly IAccountEventStoreService _eventStore;

    public AddShippingAddressInteractor(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task InteractAsync(Command.AddShippingAddress command, CancellationToken cancellationToken)
    {
        var account = await _eventStore.LoadAggregateAsync(command.AccountId, cancellationToken);
        account.Handle(command);
        await _eventStore.AppendEventsAsync(account, cancellationToken);
    }
}
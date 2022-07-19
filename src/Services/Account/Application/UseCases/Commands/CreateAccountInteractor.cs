using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CreateAccountInteractor : IInteractor<Command.CreateAccount>
{
    private readonly IAccountEventStoreService _eventStore;

    public CreateAccountInteractor(IAccountEventStoreService eventStore)
        => _eventStore = eventStore;

    public async Task InteractAsync(Command.CreateAccount command, CancellationToken cancellationToken)
    {
        Account account = new();
        account.Handle(command);
        await _eventStore.AppendEventsAsync(account, cancellationToken);
    }
}
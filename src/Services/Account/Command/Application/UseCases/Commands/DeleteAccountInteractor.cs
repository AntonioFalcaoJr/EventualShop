using Application.Abstractions;
using Application.Services;
using Contracts.Services.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class DeleteAccountInteractor : IInteractor<Command.DeleteAccount>
{
    private readonly IApplicationService _service;

    public DeleteAccountInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(Command.DeleteAccount command, CancellationToken cancellationToken)
    {
        var account = await _service.LoadAggregateAsync<Account>(command.AccountId, cancellationToken);
        account.Handle(command);
        await _service.AppendEventsAsync(account, cancellationToken);
    }
}
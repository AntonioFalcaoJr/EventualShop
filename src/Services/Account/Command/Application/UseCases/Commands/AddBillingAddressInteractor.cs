using Application.Abstractions;
using Application.Services;
using Contracts.Services.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddBillingAddressInteractor : IInteractor<Command.AddBillingAddress>
{
    private readonly IApplicationService _service;

    public AddBillingAddressInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(Command.AddBillingAddress command, CancellationToken cancellationToken)
    {
        var account = await _service.LoadAggregateAsync<Account>(command.AccountId, cancellationToken);
        account.Handle(command);
        await _service.AppendEventsAsync(account, cancellationToken);
    }
}
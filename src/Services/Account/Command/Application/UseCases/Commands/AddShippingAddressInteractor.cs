using Application.Abstractions;
using Application.Services;
using Contracts.Services.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddShippingAddressInteractor : IInteractor<Command.AddShippingAddress>
{
    private readonly IApplicationService _service;

    public AddShippingAddressInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(Command.AddShippingAddress command, CancellationToken cancellationToken)
    {
        var account = await _service.LoadAggregateAsync<Account>(command.AccountId, cancellationToken);
        account.Handle(command);
        await _service.AppendEventsAsync(account, cancellationToken);
    }
}
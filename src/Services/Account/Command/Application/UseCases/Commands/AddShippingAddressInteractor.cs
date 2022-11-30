using Application.Abstractions;
using Application.Services;
using Contracts.Services.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddShippingAddressInteractor : IInteractor<Command.AddShippingAddress>
{
    private readonly IApplicationService _applicationService;

    public AddShippingAddressInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.AddShippingAddress command, CancellationToken cancellationToken)
    {
        var account = await _applicationService.LoadAggregateAsync<Account>(command.AccountId, cancellationToken);
        account.Handle(command);
        await _applicationService.AppendEventsAsync(account, cancellationToken);
    }
}
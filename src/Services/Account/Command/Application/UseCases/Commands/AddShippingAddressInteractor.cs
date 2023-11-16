using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddShippingAddressInteractor(IApplicationService service) : IInteractor<Command.AddShippingAddress>
{
    public async Task InteractAsync(Command.AddShippingAddress command, CancellationToken cancellationToken)
    {
        var account = await service.LoadAggregateAsync<Account>(command.AccountId, cancellationToken);
        account.Handle(command);
        await service.AppendEventsAsync(account, cancellationToken);
    }
}
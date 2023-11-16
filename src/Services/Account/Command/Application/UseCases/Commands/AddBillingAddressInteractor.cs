using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Account;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddBillingAddressInteractor(IApplicationService service) : IInteractor<Command.AddBillingAddress>
{
    public async Task InteractAsync(Command.AddBillingAddress command, CancellationToken cancellationToken)
    {
        
        Ulid ulid = Ulid.NewUlid();
        
        var account = await service.LoadAggregateAsync<Account>(command.AccountId, cancellationToken);
        account.Handle(command);
        await service.AppendEventsAsync(account, cancellationToken);
    }
}